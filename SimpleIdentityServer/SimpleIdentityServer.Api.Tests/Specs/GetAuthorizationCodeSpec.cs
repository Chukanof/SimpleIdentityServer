﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Microsoft.Practices.Unity;

using NUnit.Framework;

using SimpleIdentityServer.Api.DTOs.Request;
using SimpleIdentityServer.Api.DTOs.Response;
using SimpleIdentityServer.Api.Tests.Common;
using SimpleIdentityServer.DataAccess.Fake;
using SimpleIdentityServer.RateLimitation.Configuration;

using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

using DOMAINS = SimpleIdentityServer.Core.Models;
using MODELS = SimpleIdentityServer.DataAccess.Fake.Models;
using System.Web;
using SimpleIdentityServer.Api.Tests.Common.Fakes;
using SimpleIdentityServer.Api.Tests.Common.Fakes.Models;

namespace SimpleIdentityServer.Api.Tests.Specs
{
    [Binding, Scope(Feature = "GetAuthorizationCode")]
    public class GetAuthorizationCodeSpec
    {
        private readonly GlobalContext _context;

        private HttpResponseMessage _responseMessage;

        private FakeUserInformation _fakeUserInformation;

        public GetAuthorizationCodeSpec(GlobalContext context)
        {
            var fakeGetRateLimitationElementOperation = new FakeGetRateLimitationElementOperation
            {
                Enabled = false
            };

            _context = context;
            _context.UnityContainer.RegisterInstance<IGetRateLimitationElementOperation>(fakeGetRateLimitationElementOperation);
        }

        [Given("a resource owner is authenticated")]
        public void GivenAResourceOwnerIsAuthenticated(Table table)
        {
            _fakeUserInformation = table.CreateInstance<FakeUserInformation>();
            var resourceOwner = new MODELS.ResourceOwner
            {
                Id = _fakeUserInformation.UserId
            };
            FakeDataSource.Instance().ResourceOwners.Add(resourceOwner);
        }

        [When("requesting an authorization code")]
        public void WhenRequestingAnAuthorizationCode(Table table)
        {
            var authorizationRequest = table.CreateInstance<AuthorizationRequest>();
            // Fake the authentication filter.
            var httpConfiguration = new HttpConfiguration();
            if (_fakeUserInformation != null)
            {
                httpConfiguration.Filters.Add(new FakeAuthenticationFilter
                {
                    ResourceOwnerId = _fakeUserInformation.UserId,
                    ResourceOwnerUserName = _fakeUserInformation.UserName
                });
            }

            using (var server = _context.CreateServer(httpConfiguration))
            {
                var httpClient = server.HttpClient;
                var url = string.Format(
                    "/authorization?scope={0}&response_type={1}&client_id={2}&redirect_uri={3}&prompt={4}&state={5}",
                    authorizationRequest.scope,
                    authorizationRequest.response_type,
                    authorizationRequest.client_id,
                    authorizationRequest.redirect_uri,
                    authorizationRequest.prompt,
                    authorizationRequest.state);
                _responseMessage = httpClient.GetAsync(url).Result;
            }
        }

        [Then("HTTP status code is (.*)")]
        public void ThenHttpStatusCodeIs(HttpStatusCode httpStatusCode)
        {
            Assert.That(_responseMessage.StatusCode, Is.EqualTo(httpStatusCode));
        }

        [Then("redirect to (.*) controller")]
        public void ThenRedirectToController(string controller)
        {
            var location = _responseMessage.Headers.Location;
            Assert.That(location.AbsolutePath, Is.EqualTo(controller));
        }

        [Then("redirect to callback (.*)")]
        public void ThenRedirectToCallback(string callback)
        {
            var location = _responseMessage.Headers.Location;
            Assert.That(location.AbsoluteUri, Is.StringStarting(callback));
        }

        [Then("the error returned is")]
        public void ThenTheErrorReturnedIs(Table table)
        {
            var errorResponseWithState = table.CreateInstance<ErrorResponseWithState>();
            var result = _responseMessage.Content.ReadAsAsync<ErrorResponseWithState>().Result;

            Assert.That(errorResponseWithState.error, Is.EqualTo(result.error));
            Assert.That(errorResponseWithState.state, Is.EqualTo(result.state));
        }

        [Then("the state (.*) is returned in the callback")]
        public void ThenTheStateIsReturnedInTheCallback(string state)
        {
            var location = _responseMessage.Headers.Location;
            var query = HttpUtility.ParseQueryString(location.Query);

            var returnedState = query["state"];
            Assert.That(returnedState, Is.EqualTo(state));
        }
    }
}