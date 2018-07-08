using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using SimpleIdentityServer.Authenticate.Basic;
using SimpleIdentityServer.Module;
using System;
using System.Collections.Generic;

namespace SimpleIdentityServer.Authenticate.LoginPassword
{
    public class LoginPasswordModule : IModule
    {
        private const string IsScimResourceAutomaticallyCreated = "IsScimResourceAutomaticallyCreated";
        private const string ScimBaseUrl = "ScimBaseUrl";
        private const string ClaimsIncludedInUserCreation = "ClaimsIncludedInUserCreation";
        private const string ClientId = "ClientId";
        private const string ClientSecret = "ClientSecret";
        private const string AuthorizationWellKnownConfiguration = "AuthorizationWellKnownConfiguration";

        public void Configure(IApplicationBuilder applicationBuilder)
        {
        }

        public void Configure(IRouteBuilder routeBuilder)
        {
        }

        public void ConfigureAuthentication(AuthenticationBuilder authBuilder, IDictionary<string, string> options = null)
        {
        }

        public void ConfigureAuthorization(AuthorizationOptions authorizationOptions, IDictionary<string, string> options = null)
        {
        }

        public void ConfigureServices(IServiceCollection services, IMvcBuilder mvcBuilder = null, IHostingEnvironment env = null, IDictionary<string, string> options = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if(mvcBuilder == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            // services.AddBasicShell(mvcBuilder, env);
        }

        public IEnumerable<string> GetOptionKeys()
        {
            return new string[0];
        }

        private static BasicAuthenticateOptions BuildOptions(IDictionary<string, string> opts)
        {
            var result = new BasicAuthenticateOptions
            {
                AuthenticationOptions = new BasicAuthenticationOptions()
            };
            if (opts.ContainsKey(ScimBaseUrl))
            {
                result.ScimBaseUrl = opts[ScimBaseUrl];
            }

            if (opts.ContainsKey(ClaimsIncludedInUserCreation))
            {

            }

            if (opts.ContainsKey(IsScimResourceAutomaticallyCreated))
            {

            }

            return result;
        }
    }
}
