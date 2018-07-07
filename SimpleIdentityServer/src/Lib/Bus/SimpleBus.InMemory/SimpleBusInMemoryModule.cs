using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using SimpleBus.Core;
using SimpleIdentityServer.Module;
using System;
using System.Collections.Generic;

namespace SimpleBus.InMemory
{
    public class SimpleBusInMemoryModule : IModule
    {
        private const string ServerName = "ServerName";

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

            services.AddSimpleBusInMemory(GetOptions(options));
        }

        public IEnumerable<string> GetOptionKeys()
        {
            return new[]
            {
                ServerName
            };
        }

        private static SimpleBusOptions GetOptions(IDictionary<string, string> opts)
        {
            var result = new SimpleBusOptions();
            if (opts.ContainsKey(ServerName))
            {
                result.ServerName = opts[ServerName];
            }

            return result;
        }
    }
}
