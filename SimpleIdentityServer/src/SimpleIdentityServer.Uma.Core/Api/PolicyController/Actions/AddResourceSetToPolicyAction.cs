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

using SimpleIdentityServer.Uma.Core.Errors;
using SimpleIdentityServer.Uma.Core.Exceptions;
using SimpleIdentityServer.Uma.Core.Helpers;
using SimpleIdentityServer.Uma.Core.Parameters;
using SimpleIdentityServer.Uma.Core.Repositories;
using System;
using System.Linq;

namespace SimpleIdentityServer.Uma.Core.Api.PolicyController.Actions
{
    public interface IAddResourceSetToPolicyAction
    {
        bool Execute(AddResourceSetParameter addResourceSetParameter);
    }

    internal class AddResourceSetToPolicyAction : IAddResourceSetToPolicyAction
    {
        #region Fields

        private readonly IPolicyRepository _policyRepository;

        private readonly IResourceSetRepository _resourceSetRepository;

        private readonly IRepositoryExceptionHelper _repositoryExceptionHelper;

        #endregion

        #region Constructor

        public AddResourceSetToPolicyAction(
            IPolicyRepository policyRepository,
            IResourceSetRepository resourceSetRepository,
            IRepositoryExceptionHelper repositoryExceptionHelper)
        {
            _policyRepository = policyRepository;
            _resourceSetRepository = resourceSetRepository;
            _repositoryExceptionHelper = repositoryExceptionHelper;
        }

        #endregion

        #region Public methods

        public bool Execute(AddResourceSetParameter addResourceSetParameter)
        {
            if (addResourceSetParameter == null)
            {
                throw new ArgumentNullException(nameof(addResourceSetParameter));
            }

            if (string.IsNullOrWhiteSpace(addResourceSetParameter.PolicyId))
            {
                throw new BaseUmaException(ErrorCodes.InvalidRequestCode,
                    string.Format(ErrorDescriptions.TheParameterNeedsToBeSpecified, Constants.AddResourceSetParameterNames.PolicyId));
            }

            if (addResourceSetParameter.ResourceSets == null  ||
                !addResourceSetParameter.ResourceSets.Any())
            {
                throw new BaseUmaException(ErrorCodes.InvalidRequestCode,
                    string.Format(ErrorDescriptions.TheParameterNeedsToBeSpecified, Constants.AddResourceSetParameterNames.ResourceSet));
            }

            var policy = _repositoryExceptionHelper.HandleException(
                    string.Format(ErrorDescriptions.TheAuthorizationPolicyCannotBeRetrieved, addResourceSetParameter.PolicyId),
                    () => _policyRepository.GetPolicy(addResourceSetParameter.PolicyId));
            if (policy == null)
            {
                return false;
            }
                        
            foreach (var resourceSetId in addResourceSetParameter.ResourceSets)
            {
                var resourceSet = _repositoryExceptionHelper.HandleException(
                    string.Format(ErrorDescriptions.TheResourceSetCannotBeRetrieved, resourceSetId),
                    () => _resourceSetRepository.GetResourceSetById(resourceSetId));
                if (resourceSet == null)
                {
                    throw new BaseUmaException(ErrorCodes.InvalidResourceSetId,
                        string.Format(ErrorDescriptions.TheResourceSetDoesntExist, resourceSetId));
                }
            }

            policy.ResourceSetIds.AddRange(addResourceSetParameter.ResourceSets);
            return _repositoryExceptionHelper.HandleException(
                ErrorDescriptions.ThePolicyCannotBeUpdated,
                () => _policyRepository.UpdatePolicy(policy));
        }

        #endregion
    }
}
