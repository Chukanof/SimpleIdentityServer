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

using SimpleIdentityServer.Uma.Core.Parameters;
using SimpleIdentityServer.Uma.Core.Models;
using SimpleIdentityServer.Uma.Host.DTOs.Requests;
using SimpleIdentityServer.Uma.Host.DTOs.Responses;

namespace SimpleIdentityServer.Uma.Host.Extensions
{
    internal static class MappingExtensions
    {
        #region To parameters

        public static AddResouceSetParameter ToParameter(this PostResourceSet postResourceSet)
        {
            return new AddResouceSetParameter
            {
                IconUri = postResourceSet.IconUri,
                Name = postResourceSet.Name,
                Scopes = postResourceSet.Scopes,
                Type = postResourceSet.Type,
                Uri = postResourceSet.Uri
            };
        }

        public static UpdateResourceSetParameter ToParameter(this PutResourceSet putResourceSet)
        {
            return new UpdateResourceSetParameter
            {
                Id = putResourceSet.Id,
                Name = putResourceSet.Name,
                IconUri = putResourceSet.IconUri,
                Scopes = putResourceSet.Scopes,
                Type = putResourceSet.Type,
                Uri = putResourceSet.Uri
            };
        }

        #endregion

        #region To responses

        public static ResourceSetResponse ToResponse(this ResourceSet resourceSet)
        {
            return new ResourceSetResponse
            {
                Id = resourceSet.Id,
                IconUri = resourceSet.IconUri,
                Name = resourceSet.Name,
                Scopes = resourceSet.Scopes,
                Type = resourceSet.Type,
                Uri = resourceSet.Uri 
            };
        }

        #endregion
    }
}