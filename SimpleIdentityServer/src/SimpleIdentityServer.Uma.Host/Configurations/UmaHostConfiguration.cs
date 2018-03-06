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

namespace SimpleIdentityServer.Uma.Host.Configurations
{
    public enum CachingTypes
    {
        INMEMORY,
        REDIS
    }
    
    public enum DbTypes
    {
        SQLSERVER,
        POSTGRES,
        INMEMORY
    }

    public class UmaHostConfiguration
    {
        public CachingTypes CachingType { get; set; }
        public string CachingConnectionString { get; set; }
        public string CachingInstanceName { get; set; }

        public DbTypes DbType { get; set; }
        public string DbConnectionString { get; set; }

        public DbTypes OauthDbType { get; set; }
        public string OautConnectionString { get; set; }

        public DbTypes EvtStoreDataSourceType { get; set; }
        public string EvtStoreConnectionString { get; set; }

        public string OpenIdWellKnownConfiguration { get; set; }
        public string OpenIdIntrospection { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public bool IsLogFileEnabled { get; set; }
        public string LogFilePathFormat { get; set; }

        public bool IsElasticSearchEnabled { get; set; }
        public string ElasticSearchUrl { get; set; }

        public bool IsDataMigrated { get; set; }
    }
}
