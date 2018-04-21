﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SimpleIdentityServer.Scim.Db.EF.Sqlite
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddScimSqliteEF(this IServiceCollection serviceCollection, string connectionString)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            serviceCollection.AddScimRepositories();
            serviceCollection.AddEntityFrameworkSqlite()
                .AddDbContext<ScimDbContext>(options =>
                    options.UseSqlite(connectionString));
            return serviceCollection;
        }
    }
}
