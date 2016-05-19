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

using System;
using Microsoft.AspNet.Authorization;
using System.Collections.Generic;
using System.Linq;

namespace SimpleIdentityServer.Uma.Authorization
{
    public class ResourceUmaAuthorizationRequirement : AuthorizationHandler<ResourceUmaAuthorizationRequirement>, IAuthorizationRequirement
    {
        private readonly string _resourceSetId;

        private readonly List<string> _scopes;

        #region Constructor

        public ResourceUmaAuthorizationRequirement(string resourceSetId, List<string> scopes)
        {
            if (string.IsNullOrWhiteSpace(resourceSetId))
            {
                throw new ArgumentNullException(nameof(resourceSetId));
            }

            if (scopes == null || !scopes.Any())
            {
                throw new ArgumentNullException(nameof(scopes));
            }

            _resourceSetId = resourceSetId;
            _scopes = scopes;
        }

        #endregion

        #region Protected methods

        protected override void Handle(AuthorizationContext context, ResourceUmaAuthorizationRequirement requirement)
        {
            var resource = context.Resource;
            context.Succeed(requirement);
        }

        #endregion
    }
}
