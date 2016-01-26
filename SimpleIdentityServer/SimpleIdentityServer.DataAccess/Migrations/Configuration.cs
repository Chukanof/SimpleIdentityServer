using System.Collections.Generic;
using System.Data.Entity.Migrations;
using SimpleIdentityServer.DataAccess.SqlServer.Models;

namespace SimpleIdentityServer.DataAccess.SqlServer.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<SimpleIdentityServerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SimpleIdentityServerContext context)
        {
            InsertClaims(context);
            InsertScopes(context);
            InsertTranslations(context);
        }

        private static void InsertClaims(SimpleIdentityServerContext context)
        {
            context.Claims.AddOrUpdate(p => p.Code,
                new Claim { Code = Core.Jwt.Constants.StandardResourceOwnerClaimNames.Subject });
        }

        private static void InsertScopes(SimpleIdentityServerContext context)
        {
            context.Scopes.AddOrUpdate(p => p.Name,
                new Models.Scope
                {
                    Name = "openid",
                    IsExposed = true,
                    IsOpenIdScope = true,
                    IsDisplayedInConsent = true,
                    Description = "access to the openid scope",
                    Type = ScopeType.ProtectedApi
                },
                new Models.Scope
                {
                    Name = "profile",
                    IsExposed = true,
                    IsOpenIdScope = true,
                    Description = "Access to the profile",
                    Claims = new List<Claim>
                    {
                        new Claim { Code = Core.Jwt.Constants.StandardResourceOwnerClaimNames.Name },
                        new Claim { Code = Core.Jwt.Constants.StandardResourceOwnerClaimNames.FamilyName },
                        new Claim { Code = Core.Jwt.Constants.StandardResourceOwnerClaimNames.GivenName },
                        new Claim { Code = Core.Jwt.Constants.StandardResourceOwnerClaimNames.MiddleName },
                        new Claim { Code = Core.Jwt.Constants.StandardResourceOwnerClaimNames.NickName },
                        new Claim { Code = Core.Jwt.Constants.StandardResourceOwnerClaimNames.PreferredUserName },
                        new Claim { Code = Core.Jwt.Constants.StandardResourceOwnerClaimNames.Profile },
                        new Claim { Code = Core.Jwt.Constants.StandardResourceOwnerClaimNames.Picture },
                        new Claim { Code = Core.Jwt.Constants.StandardResourceOwnerClaimNames.WebSite },
                        new Claim { Code = Core.Jwt.Constants.StandardResourceOwnerClaimNames.Gender },
                        new Claim { Code = Core.Jwt.Constants.StandardResourceOwnerClaimNames.BirthDate },
                        new Claim { Code = Core.Jwt.Constants.StandardResourceOwnerClaimNames.ZoneInfo },
                        new Claim { Code = Core.Jwt.Constants.StandardResourceOwnerClaimNames.Locale },
                        new Claim { Code = Core.Jwt.Constants.StandardResourceOwnerClaimNames.UpdatedAt }
                    },
                    Type = ScopeType.ResourceOwner,
                    IsDisplayedInConsent = true
                }, 
                new Models.Scope
                {
                    Name = "email",
                    IsExposed = true,
                    IsOpenIdScope = true,
                    IsDisplayedInConsent = true,
                    Description = "Access to the email",
                    Claims = new List<Claim>
                    {
                        new Claim { Code = Core.Jwt.Constants.StandardResourceOwnerClaimNames.Email },
                        new Claim { Code = Core.Jwt.Constants.StandardResourceOwnerClaimNames.EmailVerified }
                    },
                    Type = ScopeType.ResourceOwner
                },
                new Models.Scope
                {
                    Name = "address",
                    IsExposed = true,
                    IsOpenIdScope = true,
                    IsDisplayedInConsent = true,
                    Description = "Access to the address",
                    Claims = new List<Claim>
                    {
                        new Claim { Code = Core.Jwt.Constants.StandardResourceOwnerClaimNames.Address }
                    },
                    Type = ScopeType.ResourceOwner
                },
                new Models.Scope
                {
                    Name = "phone",
                    IsExposed = true,
                    IsOpenIdScope = true,
                    IsDisplayedInConsent = true,
                    Description = "Access to the phone",
                    Claims = new List<Claim>
                    {
                        new Claim { Code = Core.Jwt.Constants.StandardResourceOwnerClaimNames.PhoneNumber },
                        new Claim { Code = Core.Jwt.Constants.StandardResourceOwnerClaimNames.PhoneNumberVerified }
                    },
                    Type = ScopeType.ResourceOwner
                });
        }

        private static void InsertTranslations(SimpleIdentityServerContext context)
        {
            context.Translations.AddOrUpdate(c => c.Code,
                new Models.Translation
                {
                    LanguageTag = "en",
                    Code = Core.Constants.StandardTranslationCodes.ApplicationWouldLikeToCode,
                    Value = "the client {0} would like to access"
                },
                new Models.Translation
                {
                    LanguageTag = "en",
                    Code = Core.Constants.StandardTranslationCodes.IndividualClaimsCode,
                    Value = "individual claims"
                },
                new Models.Translation
                {
                    LanguageTag = "en",
                    Code = Core.Constants.StandardTranslationCodes.LoginCode,
                    Value = "Login"
                },
                new Models.Translation
                {
                    LanguageTag = "en",
                    Code = Core.Constants.StandardTranslationCodes.PasswordCode,
                    Value = "Password"
                },
                new Models.Translation
                {
                    LanguageTag = "en",
                    Code = Core.Constants.StandardTranslationCodes.UserNameCode,
                    Value = "Username"
                },
                new Models.Translation
                {
                    LanguageTag = "en",
                    Code = Core.Constants.StandardTranslationCodes.ConfirmCode,
                    Value = "Confirm"
                },
                new Models.Translation
                {
                    LanguageTag = "en",
                    Code = Core.Constants.StandardTranslationCodes.CancelCode,
                    Value = "Cancel"
                },
                new Models.Translation
                {
                    LanguageTag = "en",
                    Code = Core.Constants.StandardTranslationCodes.LoginLocalAccount,
                    Value = "Login with your local account"
                },
                new Models.Translation
                {
                    LanguageTag = "en",
                    Code = Core.Constants.StandardTranslationCodes.LoginExternalAccount,
                    Value = "Login with your external account"
                },
                new Models.Translation
                {
                    LanguageTag = "en",
                    Code = Core.Constants.StandardTranslationCodes.LinkToThePolicy,
                    Value = "policy"
                },
                new Models.Translation
                {
                    LanguageTag = "en",
                    Code = Core.Constants.StandardTranslationCodes.Tos,
                    Value = "Terms of Service"
                },
                // Swedish
                new Models.Translation
                {
                    LanguageTag = "se",
                    Code = Core.Constants.StandardTranslationCodes.ApplicationWouldLikeToCode,
                    Value = "till�mpning {0} skulle vilja:"
                },
                new Models.Translation
                {
                    LanguageTag = "se",
                    Code = Core.Constants.StandardTranslationCodes.IndividualClaimsCode,
                    Value = "enskilda anspr�k"
                },
                new Models.Translation
                {
                    LanguageTag = "se",
                    Code = Core.Constants.StandardTranslationCodes.LoginCode,
                    Value = "Logga in"
                },
                new Models.Translation
                {
                    LanguageTag = "se",
                    Code = Core.Constants.StandardTranslationCodes.PasswordCode,
                    Value = "L�senord"
                },
                new Models.Translation
                {
                    LanguageTag = "se",
                    Code = Core.Constants.StandardTranslationCodes.UserNameCode,
                    Value = "Anv�ndarnamn"
                },
                new Models.Translation
                {
                    LanguageTag = "se",
                    Code = Core.Constants.StandardTranslationCodes.ConfirmCode,
                    Value = "bekr�fta"
                },
                new Models.Translation
                {
                    LanguageTag = "se",
                    Code = Core.Constants.StandardTranslationCodes.CancelCode,
                    Value = "annullera"
                },
                // French                
                new Models.Translation
                {
                    LanguageTag = "fr",
                    Code = Core.Constants.StandardTranslationCodes.ApplicationWouldLikeToCode,
                    Value = "L'application veut acc�der �:"
                },
                new Models.Translation
                {
                    LanguageTag = "fr",
                    Code = Core.Constants.StandardTranslationCodes.IndividualClaimsCode,
                    Value = "Les claims"
                },
                new Models.Translation
                {
                    LanguageTag = "fr",
                    Code = Core.Constants.StandardTranslationCodes.LoginCode,
                    Value = "S'authentifier"
                },
                new Models.Translation
                {
                    LanguageTag = "fr",
                    Code = Core.Constants.StandardTranslationCodes.PasswordCode,
                    Value = "Mot de passe"
                },
                new Models.Translation
                {
                    LanguageTag = "fr",
                    Code = Core.Constants.StandardTranslationCodes.UserNameCode,
                    Value = "Nom d'utilisateur"
                },
                new Models.Translation
                {
                    LanguageTag = "fr",
                    Code = Core.Constants.StandardTranslationCodes.ConfirmCode,
                    Value = "confirmer"
                },
                new Models.Translation
                {
                    LanguageTag = "fr",
                    Code = Core.Constants.StandardTranslationCodes.CancelCode,
                    Value = "annuler"
                });
        }
    }
}
