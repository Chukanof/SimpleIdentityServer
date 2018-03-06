﻿#region copyright
// Copyright 2015 Habart Thierry
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using SimpleIdentityServer.DataAccess.SqlServer;
using SimpleIdentityServer.Oauth2Instrospection.Authentication;
using SimpleIdentityServer.Uma.EF;
using SimpleIdentityServer.Uma.Host.Configurations;
using SimpleIdentityServer.Uma.Host.Middlewares;
using SimpleIdentityServer.Uma.Logging;
using System;

namespace SimpleIdentityServer.Uma.Host.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseUmaHost(this IApplicationBuilder app, ILoggerFactory loggerFactory, UmaHostConfiguration configuration)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (loggerFactory != null)
            {
                loggerFactory.AddSerilog();
            }

            // 1. Display status code page.
            app.UseStatusCodePages();
            // 2. Enable OAUTH authentication.
            var introspectionOptions = new Oauth2IntrospectionOptions
            {
                InstrospectionEndPoint = configuration.AuthorizationServer.IntrospectionEndpoints,
                ClientId = configuration.AuthorizationServer.ClientId,
                ClientSecret = configuration.AuthorizationServer.ClientSecret
            };
            app.UseAuthenticationWithIntrospection(introspectionOptions);
            // 3. Insert seed data
            if (configuration.DataSource.IsUmaMigrated)
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var simpleIdServerUmaContext = serviceScope.ServiceProvider.GetService<SimpleIdServerUmaContext>();
                    try
                    {
                        simpleIdServerUmaContext.Database.EnsureCreated();
                    }
                    catch (Exception) { }
                }
            }

            if (configuration.DataSource.IsOauthMigrated)
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var simpleIdentityServerContext = serviceScope.ServiceProvider.GetService<SimpleIdentityServerContext>();
                    simpleIdentityServerContext.Database.EnsureCreated();
                }
            }

            // 4. Enable CORS
            app.UseCors("AllowAll");
            // 5. Display exception
            app.UseUmaExceptionHandler(new ExceptionHandlerMiddlewareOptions
            {
                UmaEventSource = app.ApplicationServices.GetService<IUmaServerEventSource>()
            });
            // 6. Launch ASP.NET MVC
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}");
            });
            return app;
        }
    }
}
