﻿using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.Unity;

using SimpleIdentityServer.Host.DTOs.Request;
using SimpleIdentityServer.Api.Tests.Common;
using SimpleIdentityServer.Api.Tests.Common.Fakes;
using SimpleIdentityServer.Api.Tests.Common.Fakes.Models;
using SimpleIdentityServer.Core.Configuration;
using SimpleIdentityServer.Core.Factories;
using SimpleIdentityServer.RateLimitation.Configuration;
using SimpleIdentityServer.RateLimitation.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System;

using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;
using DOMAINS = SimpleIdentityServer.Core.Models;
using HttpClientFactory = System.Net.Http.HttpClientFactory;
using MODELS = SimpleIdentityServer.DataAccess.Fake.Models;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleIdentityServer.Api.Tests.Specs
{
    [Binding, Scope(Feature = "GetAccessTokenMultipleTime")]
    public sealed class GetAccessTokenMultipleTimeSpec
    {
        private readonly GlobalContext _globalContext;

        private List<DOMAINS.GrantedToken> _tokens;

        private List<FakeTooManyRequestResponse> _errors;

        private RateLimitationElement _rateLimitationElement;

        private List<FakeHttpResponse> _httpResponses;

        public GetAccessTokenMultipleTimeSpec(GlobalContext globalContext)
        {
            _globalContext = globalContext;

            _rateLimitationElement = new RateLimitationElement
            {
                Name = "PostToken"
            };
            
            _tokens = new List<DOMAINS.GrantedToken>();
            _errors = new List<FakeTooManyRequestResponse>();
            _httpResponses = new List<FakeHttpResponse>();

            _globalContext.CreateServer(services =>
            {
                var fakeGetRateLimitationElementOperation = new FakeGetRateLimitationElementOperation
                {
                    Enabled = true,
                    RateLimitationElement = _rateLimitationElement
                };
                services.AddInstance<IGetRateLimitationElementOperation>(fakeGetRateLimitationElementOperation);
                services.AddTransient<ISimpleIdentityServerConfigurator, SimpleIdentityServerConfigurator>();
            });
            _globalContext.AuthenticationMiddleWareOptions.IsEnabled = true;
            _globalContext.AuthenticationMiddleWareOptions.Subject = "subject";
        }

        [Given("allowed number of requests is (.*)")]
        public void GivenAllowedNumberOfRequests(int numberOfRequests)
        {
            _rateLimitationElement.NumberOfRequests = numberOfRequests;
        }

        [Given("sliding time is (.*)")]
        public void GivenSlidingTime(double slidingTime)
        {
            _rateLimitationElement.SlidingTime = slidingTime;
        }

        [When("requesting access tokens")]
        public void WhenRequestingAccessTokens(Table table)
        {
            var tokenRequests = table.CreateSet<TokenRequest>();
            var responseCacheManager = _globalContext.ServiceProvider.GetService<ICacheManagerProvider>().GetCacheManager();
            responseCacheManager.Flush();

            var httpClient = _globalContext.TestServer.CreateClient();
            foreach (var tokenRequest in tokenRequests)
            {
                var parameter = string.Format(
                    "grant_type=password&username={0}&password={1}&client_id={2}&scope={3}",
                    tokenRequest.username,
                    tokenRequest.password,
                    tokenRequest.client_id,
                    tokenRequest.scope);
                var content = new StringContent(parameter, Encoding.UTF8, "application/x-www-form-urlencoded");
            
                var result = httpClient.PostAsync("/token", content).Result;
                var httpStatusCode = result.StatusCode;
                _httpResponses.Add(new FakeHttpResponse
                {
                    StatusCode = httpStatusCode,
                    NumberOfRequests = result.Headers.GetValues(RateLimitationConstants.XRateLimitLimitName).FirstOrDefault(),
                    NumberOfRemainingRequests = result.Headers.GetValues(RateLimitationConstants.XRateLimitRemainingName).FirstOrDefault()
                });
                if (httpStatusCode == HttpStatusCode.OK)
                {
                    _tokens.Add(result.Content.ReadAsAsync<DOMAINS.GrantedToken>().Result);
                    continue;
                }
                
                _errors.Add(new FakeTooManyRequestResponse
                {
                    Message = result.Content.ReadAsStringAsync().Result,
                });
            }
        }

        [Then("(.*) access tokens are generated")]
        public void ThenTheResultShouldBe(int numberOfAccessTokens)
        {
            Assert.True(_tokens.Count.Equals(numberOfAccessTokens));
        }

        [Then("the errors should be returned")]
        public void ThenErrorsShouldBe(Table table)
        {
            var records = table.CreateSet<FakeTooManyRequestResponse>().ToList();
            Assert.True(records.Count.Equals(_errors.Count()));
            for (var i = 0; i < records.Count() - 1; i++)
            {
                var record = records[i];
                var error = _errors[i];
                Assert.True(record.Message.Equals(error.Message));
            }
        }

        [Then("the http responses should be returned")]
        public void ThenHttpHeadersShouldContain(Table table)
        {
            var records = table.CreateSet<FakeHttpResponse>().ToList();
            Assert.True(records.Count.Equals(_httpResponses.Count()));
            for(var i = 0; i < records.Count() - 1; i++)
            {
                var record = records[i];
                var httpResponse = _httpResponses[i];
                Assert.True(record.StatusCode.Equals(record.StatusCode));
                Assert.True(record.NumberOfRemainingRequests.Equals(record.NumberOfRemainingRequests));
                Assert.True(record.NumberOfRequests.Equals(record.NumberOfRequests));
            }
        }
    }
}
