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

using Moq;
using SimpleIdentityServer.Core.Api.Authorization;
using SimpleIdentityServer.Core.Common;
using SimpleIdentityServer.Core.Common.Extensions;
using SimpleIdentityServer.Core.Common.Models;
using SimpleIdentityServer.Core.Helpers;
using SimpleIdentityServer.Core.JwtToken;
using SimpleIdentityServer.Core.Parameters;
using SimpleIdentityServer.Core.Results;
using SimpleIdentityServer.OAuth.Logging;
using SimpleIdentityServer.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimpleIdentityServer.Core.UnitTests.Common
{
    public sealed  class GenerateAuthorizationResponseFixture
    {
        private Mock<IAuthorizationCodeStore> _authorizationCodeRepositoryFake;
        private Mock<IParameterParserHelper> _parameterParserHelperFake;
        private Mock<IJwtGenerator> _jwtGeneratorFake;
        private Mock<IGrantedTokenGeneratorHelper> _grantedTokenGeneratorHelperFake;
        private Mock<ITokenStore> _grantedTokenRepositoryFake;
        private Mock<IConsentHelper> _consentHelperFake;
        private Mock<IOAuthEventSource> _oauthEventSource;
        private Mock<IAuthorizationFlowHelper> _authorizationFlowHelperFake;                
        private Mock<IClientHelper> _clientHelperFake;
        private Mock<IGrantedTokenHelper> _grantedTokenHelperStub;
        private IGenerateAuthorizationResponse _generateAuthorizationResponse;

        public static string ToHexString(IEnumerable<byte> arr)
        {
            if (arr == null)
            {
                throw new ArgumentNullException(nameof(arr));
            }

            var sb = new StringBuilder();
            foreach (var s in arr)
            {
                sb.Append(s.ToString("x2"));
            }

            return sb.ToString();
        }

        [Fact]
        public void WhenGenerateSessionState()
        {
            // 0cb582e736597d717f0f6b34b987ea5ad0a6a82c1294fa37e2d75a444da782aa
            // 0cb582e736597d717f0f6b34b987ea5ad0a6a82c1294fa37e2d75a444da782aa
            const string clientId = "ResourceManagerClientId";
            const string originUrl = "http://localhost:64950";
            const string sessionId = "d95d6ea3-36f5-4ccd-886a-d469210f8e33";
            const string salt = "a781f21b-a9e0-4b84-90ed-2dc4535ac927";
            var bytes = Encoding.UTF8.GetBytes(clientId + originUrl + sessionId + salt);
            byte[] hash;
            using (var sha = SHA256.Create())
            {
                hash = sha.ComputeHash(bytes);
            }

            var hashed = ToHexString(hash);
            var b = hashed.Base64Encode();
            string s = "";
        }

        [Fact]
        public async Task When_Passing_No_Action_Result_Then_Exception_Is_Thrown()
        {
            // ARRANGE
            InitializeFakeObjects();

            // ACT & ASSERT
            await Assert.ThrowsAsync<ArgumentNullException>(() => _generateAuthorizationResponse.ExecuteAsync(null, null, null, null));
        }

        [Fact]
        public async Task When_Passing_No_Authorization_Request_Then_Exception_Is_Thrown()
        {
            // ARRANGE
            InitializeFakeObjects();
            var redirectInstruction = new ActionResult
            {
                RedirectInstruction = new RedirectInstruction()
            };
            
            // ACT & ASSERT
            await Assert.ThrowsAsync<ArgumentNullException>(() => _generateAuthorizationResponse.ExecuteAsync(redirectInstruction, null, null, null));
        }

        [Fact]
        public async Task When_There_Is_No_Logged_User_Then_Exception_Is_Throw()
        {
            // ARRANGE
            InitializeFakeObjects();
            var redirectInstruction = new ActionResult
            {
                RedirectInstruction = new RedirectInstruction()
            };

            // ACT & ASSERT
            await Assert.ThrowsAsync<ArgumentNullException>(() => _generateAuthorizationResponse.ExecuteAsync(redirectInstruction, new AuthorizationParameter(), null, null));
        }

        [Fact]
        public async Task When_No_Client_Is_Passed_Then_Exception_Is_Thrown()
        {
            // ARRANGE
            InitializeFakeObjects();
            var redirectInstruction = new ActionResult
            {
                RedirectInstruction = new RedirectInstruction()
            };
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity("fake"));

            // ACT & ASSERT
            await Assert.ThrowsAsync<ArgumentNullException>(() => _generateAuthorizationResponse.ExecuteAsync(redirectInstruction, new AuthorizationParameter(), claimsPrincipal, null));
        }

        [Fact]
        public async Task When_Generating_AuthorizationResponse_With_IdToken_Then_IdToken_Is_Added_To_The_Parameters()
        {
            // ARRANGE
            InitializeFakeObjects();
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity("fake"));
            const string idToken = "idToken";
            var authorizationParameter = new AuthorizationParameter();
            var actionResult = new ActionResult
            {
                RedirectInstruction = new RedirectInstruction()
            };
            var jwsPayload = new JwsPayload();
            _parameterParserHelperFake.Setup(p => p.ParseResponseTypes(It.IsAny<string>()))
                .Returns(new List<ResponseType>
                {
                    ResponseType.id_token  
                });
            _jwtGeneratorFake.Setup(
                j => j.GenerateIdTokenPayloadForScopesAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthorizationParameter>()))
                .Returns(Task.FromResult(jwsPayload));
            _jwtGeneratorFake.Setup(
                j => j.GenerateUserInfoPayloadForScopeAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthorizationParameter>()))
                .Returns(Task.FromResult(jwsPayload));
            _jwtGeneratorFake.Setup(j => j.EncryptAsync(It.IsAny<string>(), It.IsAny<JweAlg>(), It.IsAny<JweEnc>()))
                .Returns(Task.FromResult(idToken));
            _clientHelperFake.Setup(c => c.GenerateIdTokenAsync(It.IsAny<string>(),
                It.IsAny<JwsPayload>()))
                .Returns(Task.FromResult(idToken));

            // ACT
            await _generateAuthorizationResponse.ExecuteAsync(actionResult, authorizationParameter, claimsPrincipal, new Core.Common.Models.Client());

            // ASSERT
            Assert.True(actionResult.RedirectInstruction.Parameters.Any(p => p.Name == Constants.StandardAuthorizationResponseNames.IdTokenName));
            Assert.True(actionResult.RedirectInstruction.Parameters.Any(p => p.Value == idToken));
        }

        [Fact]
        public async Task When_Generating_AuthorizationResponse_With_AccessToken_And_ThereIs_No_Granted_Token_Then_Token_Is_Generated_And_Added_To_The_Parameters()
        {
            // ARRANGE
            InitializeFakeObjects();
            const string idToken = "idToken";
            const string clientId = "clientId";
            const string scope = "openid";
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity("fake"));
            var authorizationParameter = new AuthorizationParameter
            {
                ClientId = clientId,
                Scope = scope
            };
            var grantedToken = new GrantedToken
            {
                AccessToken = Guid.NewGuid().ToString()
            };
            var actionResult = new ActionResult
            {
                RedirectInstruction = new RedirectInstruction()
            };
            var jwsPayload = new JwsPayload();
            _parameterParserHelperFake.Setup(p => p.ParseResponseTypes(It.IsAny<string>()))
                .Returns(new List<ResponseType>
                {
                    ResponseType.token  
                });
            _jwtGeneratorFake.Setup(
                j => j.GenerateIdTokenPayloadForScopesAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthorizationParameter>()))
                .Returns(Task.FromResult(jwsPayload));
            _jwtGeneratorFake.Setup(
                j => j.GenerateUserInfoPayloadForScopeAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthorizationParameter>()))
                .Returns(Task.FromResult(jwsPayload));
            _jwtGeneratorFake.Setup(j => j.EncryptAsync(It.IsAny<string>(), It.IsAny<JweAlg>(), It.IsAny<JweEnc>()))
                .Returns(Task.FromResult(idToken));
            _parameterParserHelperFake.Setup(p => p.ParseScopes(It.IsAny<string>()))
                .Returns(() => new List<string> { scope });
            _grantedTokenHelperStub.Setup(r => r.GetValidGrantedTokenAsync(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<JwsPayload>(),
                It.IsAny<JwsPayload>()))
                .Returns(Task.FromResult((GrantedToken)null));
            _grantedTokenGeneratorHelperFake.Setup(r => r.GenerateTokenAsync(It.IsAny<Core.Common.Models.Client>(),
                It.IsAny<string>(),
                It.IsAny<JwsPayload>(),
                It.IsAny<JwsPayload>()))
                .Returns(Task.FromResult(grantedToken));

            // ACT
            await _generateAuthorizationResponse.ExecuteAsync(actionResult, authorizationParameter, claimsPrincipal, new Core.Common.Models.Client());

            // ASSERTS
            Assert.True(actionResult.RedirectInstruction.Parameters.Any(p => p.Name == Core.Constants.StandardAuthorizationResponseNames.AccessTokenName));
            Assert.True(actionResult.RedirectInstruction.Parameters.Any(p => p.Value == grantedToken.AccessToken));
            _grantedTokenRepositoryFake.Verify(g => g.AddToken(grantedToken));
            _oauthEventSource.Verify(e => e.GrantAccessToClient(clientId, grantedToken.AccessToken, scope));
        }

        [Fact]
        public async Task When_Generating_AuthorizationResponse_With_AccessToken_And_ThereIs_A_GrantedToken_Then_Token_Is_Added_To_The_Parameters()
        {
            // ARRANGE
            InitializeFakeObjects();
            const string idToken = "idToken";
            const string clientId = "clientId";
            const string scope = "openid";
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity("fake"));
            var authorizationParameter = new AuthorizationParameter
            {
                ClientId = clientId,
                Scope = scope
            };
            var grantedToken = new GrantedToken
            {
                AccessToken = Guid.NewGuid().ToString()
            };
            var actionResult = new ActionResult
            {
                RedirectInstruction = new RedirectInstruction()
            };
            var jwsPayload = new JwsPayload();
            _parameterParserHelperFake.Setup(p => p.ParseResponseTypes(It.IsAny<string>()))
                .Returns(new List<ResponseType>
                {
                    ResponseType.token  
                });
            _jwtGeneratorFake.Setup(
                j => j.GenerateIdTokenPayloadForScopesAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthorizationParameter>()))
                .Returns(Task.FromResult(jwsPayload));
            _jwtGeneratorFake.Setup(
                j => j.GenerateUserInfoPayloadForScopeAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthorizationParameter>()))
                .Returns(Task.FromResult(jwsPayload));
            _jwtGeneratorFake.Setup(j => j.EncryptAsync(It.IsAny<string>(), It.IsAny<JweAlg>(), It.IsAny<JweEnc>()))
                .Returns(Task.FromResult(idToken));
            _parameterParserHelperFake.Setup(p => p.ParseScopes(It.IsAny<string>()))
                .Returns(() => new List<string> { scope });
            _grantedTokenHelperStub.Setup(r => r.GetValidGrantedTokenAsync(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<JwsPayload>(),
                It.IsAny<JwsPayload>()))
                .Returns(() => Task.FromResult(grantedToken));

            // ACT
            await _generateAuthorizationResponse.ExecuteAsync(actionResult, authorizationParameter, claimsPrincipal, new Core.Common.Models.Client());

            // ASSERTS
            Assert.True(actionResult.RedirectInstruction.Parameters.Any(p => p.Name == Core.Constants.StandardAuthorizationResponseNames.AccessTokenName));
            Assert.True(actionResult.RedirectInstruction.Parameters.Any(p => p.Value == grantedToken.AccessToken));
        }

        [Fact]
        public async Task When_Generating_AuthorizationResponse_With_AuthorizationCode_Then_Code_Is_Added_To_The_Parameters()
        {
            // ARRANGE
            InitializeFakeObjects();
            const string idToken = "idToken";
            const string clientId = "clientId";
            const string scope = "openid";
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity("fake"));
            var authorizationParameter = new AuthorizationParameter
            {
                ClientId = clientId,
                Scope = scope
            };
            var consent = new Consent();
            var actionResult = new ActionResult
            {
                RedirectInstruction = new RedirectInstruction()
            };
            var jwsPayload = new JwsPayload();
            _parameterParserHelperFake.Setup(p => p.ParseResponseTypes(It.IsAny<string>()))
                .Returns(new List<ResponseType>
                {
                    ResponseType.code  
                });
            _jwtGeneratorFake.Setup(
                j => j.GenerateIdTokenPayloadForScopesAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthorizationParameter>()))
                .Returns(Task.FromResult(jwsPayload));
            _jwtGeneratorFake.Setup(
                j => j.GenerateUserInfoPayloadForScopeAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthorizationParameter>()))
                .Returns(Task.FromResult(jwsPayload));
            _jwtGeneratorFake.Setup(j => j.EncryptAsync(It.IsAny<string>(), It.IsAny<JweAlg>(), It.IsAny<JweEnc>()))
                .Returns(Task.FromResult(idToken));
            _consentHelperFake.Setup(c => c.GetConfirmedConsentsAsync(It.IsAny<string>(),
                It.IsAny<AuthorizationParameter>()))
                .Returns(Task.FromResult(consent));

            // ACT
            await _generateAuthorizationResponse.ExecuteAsync(actionResult, authorizationParameter, claimsPrincipal, new Core.Common.Models.Client());

            // ASSERTS
            Assert.True(actionResult.RedirectInstruction.Parameters.Any(p => p.Name == Core.Constants.StandardAuthorizationResponseNames.AuthorizationCodeName));
            _authorizationCodeRepositoryFake.Verify(a => a.AddAuthorizationCode(It.IsAny<AuthorizationCode>()));
            _oauthEventSource.Verify(s => s.GrantAuthorizationCodeToClient(clientId, It.IsAny<string>(), scope));
        }

        [Fact]
        public async Task When_An_Authorization_Response_Is_Generated_Then_Events_Are_Logged()
        {
            // ARRANGE
            InitializeFakeObjects();
            const string idToken = "idToken";
            const string clientId = "clientId";
            const string scope = "scope";
            const string responseType = "id_token";
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity("fake"));
            var authorizationParameter = new AuthorizationParameter
            {
                ClientId = clientId,
                Scope = scope,
                ResponseType = responseType
            };
            var client = new Core.Common.Models.Client
            {
                IdTokenEncryptedResponseAlg = Jwt.Constants.JweAlgNames.RSA1_5,
                IdTokenEncryptedResponseEnc = Jwt.Constants.JweEncNames.A128CBC_HS256,
                IdTokenSignedResponseAlg = Jwt.Constants.JwsAlgNames.RS256
            };
            var actionResult = new ActionResult
            {
                RedirectInstruction = new RedirectInstruction()
            };
            var jwsPayload = new JwsPayload();
            _parameterParserHelperFake.Setup(p => p.ParseResponseTypes(It.IsAny<string>()))
                .Returns(new List<ResponseType>
                {
                    ResponseType.id_token  
                });
            _jwtGeneratorFake.Setup(
                j => j.GenerateIdTokenPayloadForScopesAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthorizationParameter>()))
                .Returns(Task.FromResult(jwsPayload));
            _jwtGeneratorFake.Setup(
                j => j.GenerateUserInfoPayloadForScopeAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthorizationParameter>()))
                .Returns(Task.FromResult(jwsPayload));
            _jwtGeneratorFake.Setup(j => j.EncryptAsync(It.IsAny<string>(), It.IsAny<JweAlg>(), It.IsAny<JweEnc>()))
                .Returns(Task.FromResult(idToken));

            // ACT
            await _generateAuthorizationResponse.ExecuteAsync(actionResult, authorizationParameter, claimsPrincipal, new Core.Common.Models.Client());

            // ASSERT
            _oauthEventSource.Verify(s => s.StartGeneratingAuthorizationResponseToClient(clientId, responseType));
            _oauthEventSource.Verify(s => s.EndGeneratingAuthorizationResponseToClient(clientId, actionResult.RedirectInstruction.Parameters.SerializeWithJavascript()));
        }

        [Fact]
        public async Task When_Redirecting_To_Callback_And_There_Is_No_Response_Mode_Specified_Then_The_Response_Mode_Is_Set()
        {
            // ARRANGE
            InitializeFakeObjects();
            const string idToken = "idToken";
            const string clientId = "clientId";
            const string scope = "scope";
            const string responseType = "id_token";
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity("fake"));
            var authorizationParameter = new AuthorizationParameter
            {
                ClientId = clientId,
                Scope = scope,
                ResponseType = responseType,
                ResponseMode = ResponseMode.None
            };
            var client = new Core.Common.Models.Client
            {
                IdTokenEncryptedResponseAlg = Jwt.Constants.JweAlgNames.RSA1_5,
                IdTokenEncryptedResponseEnc = Jwt.Constants.JweEncNames.A128CBC_HS256,
                IdTokenSignedResponseAlg = Jwt.Constants.JwsAlgNames.RS256
            };
            var actionResult = new ActionResult
            {
                RedirectInstruction = new RedirectInstruction(),
                Type = TypeActionResult.RedirectToCallBackUrl
            };
            var jwsPayload = new JwsPayload();
            _parameterParserHelperFake.Setup(p => p.ParseResponseTypes(It.IsAny<string>()))
                .Returns(new List<ResponseType>
                {
                    ResponseType.id_token  
                });
            _jwtGeneratorFake.Setup(
                j => j.GenerateIdTokenPayloadForScopesAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthorizationParameter>()))
                .Returns(Task.FromResult(jwsPayload));
            _jwtGeneratorFake.Setup(
                j => j.GenerateUserInfoPayloadForScopeAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthorizationParameter>()))
                .Returns(Task.FromResult(jwsPayload));
            _jwtGeneratorFake.Setup(j => j.EncryptAsync(It.IsAny<string>(), It.IsAny<JweAlg>(), It.IsAny<JweEnc>()))
                .Returns(Task.FromResult(idToken));
            _authorizationFlowHelperFake.Setup(
                a => a.GetAuthorizationFlow(It.IsAny<ICollection<ResponseType>>(), It.IsAny<string>()))
                .Returns(AuthorizationFlow.ImplicitFlow);

            // ACT
            await _generateAuthorizationResponse.ExecuteAsync(actionResult, authorizationParameter, claimsPrincipal, new Core.Common.Models.Client());

            // ASSERT
            Assert.True(actionResult.RedirectInstruction.ResponseMode == ResponseMode.fragment);
        }

        private void InitializeFakeObjects()
        {
            _authorizationCodeRepositoryFake = new Mock<IAuthorizationCodeStore>();
            _parameterParserHelperFake = new Mock<IParameterParserHelper>();
            _jwtGeneratorFake = new Mock<IJwtGenerator>();
            _grantedTokenGeneratorHelperFake = new Mock<IGrantedTokenGeneratorHelper>();
            _grantedTokenRepositoryFake = new Mock<ITokenStore>();
            _consentHelperFake = new Mock<IConsentHelper>();
            _oauthEventSource = new Mock<IOAuthEventSource>();
            _authorizationFlowHelperFake = new Mock<IAuthorizationFlowHelper>();
            _clientHelperFake = new Mock<IClientHelper>();
            _grantedTokenHelperStub = new Mock<IGrantedTokenHelper>();
            _generateAuthorizationResponse = new GenerateAuthorizationResponse(
                _authorizationCodeRepositoryFake.Object,
                _grantedTokenRepositoryFake.Object,
                _parameterParserHelperFake.Object,
                _jwtGeneratorFake.Object,
                _grantedTokenGeneratorHelperFake.Object,
                _consentHelperFake.Object,
                _oauthEventSource.Object,
                _authorizationFlowHelperFake.Object,
                _clientHelperFake.Object,
                _grantedTokenHelperStub.Object);
        }
    }
}
