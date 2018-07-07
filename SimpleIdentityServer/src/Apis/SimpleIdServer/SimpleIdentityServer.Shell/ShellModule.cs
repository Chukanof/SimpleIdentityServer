using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using SimpleIdentityServer.Module;
using System;
using System.Collections.Generic;

namespace SimpleIdentityServer.Shell
{
    public class ShellModule : IModule
    {
        public void Configure(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseShellStaticFiles();
        }

        public void Configure(IRouteBuilder routeBuilder)
        {
            routeBuilder.UseShell();
        }

        public void ConfigureAuthentication(AuthenticationBuilder authBuilder, IDictionary<string, string> options = null)
        {
        }

        public void ConfigureAuthorization(AuthorizationOptions authorizationOptions, IDictionary<string, string> options = null)
        {
        }

        public void ConfigureServices(IServiceCollection services, IMvcBuilder mvcBuilder = null, IHostingEnvironment env, IDictionary<string, string> options = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if(mvcBuilder == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddBasicShell(mvcBuilder, mvcBuilder);
        }

        public IEnumerable<string> GetOptionKeys()
        {
            return new string[0];
        }
    }
}
