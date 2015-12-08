﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace SimpleIdentityServer.Api.Tests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("GetUserInformation")]
    public partial class GetUserInformationFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "GetUserInformation.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "GetUserInformation", "A resource owner is authenticated\r\nA user is trying to fetch the resource owner i" +
                    "nformation from the user info endpoint.", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Fetch the user information for the scope profile")]
        public virtual void FetchTheUserInformationForTheScopeProfile()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Fetch the user information for the scope profile", ((string[])(null)));
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("a mobile application MyHolidays is defined", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.And("set the name of the issuer http://localhost/identity", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
 testRunner.And("the redirection uri http://localhost is assigned to the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "IsInternal",
                        "Claims"});
            table1.AddRow(new string[] {
                        "openid",
                        "true",
                        ""});
            table1.AddRow(new string[] {
                        "profile",
                        "true",
                        "name family_name given_name middle_name nickname preferred_username profile pictu" +
                            "re website gender birthdate zoneinfo locale updated_at"});
#line 11
 testRunner.And("the scopes are defined", ((string)(null)), table1, "And ");
#line 15
 testRunner.And("the scopes openid,profile are assigned to the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
 testRunner.And("the grant-type implicit is supported by the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
 testRunner.And("the response-types token,id_token are supported by the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Name",
                        "GivenName",
                        "FamilyName",
                        "MiddleName",
                        "NickName",
                        "PreferredUserName",
                        "Profile",
                        "Picture",
                        "WebSite",
                        "Email",
                        "EmailVerified",
                        "Gender",
                        "BirthDate",
                        "ZoneInfo",
                        "Locale",
                        "PhoneNumber",
                        "PhoneNumberVerified"});
            table2.AddRow(new string[] {
                        "habarthierry@loki.be",
                        "thabart",
                        "givename",
                        "familyname",
                        "middlename",
                        "nickname",
                        "preferredusername",
                        "profile",
                        "picture",
                        "website",
                        "email",
                        "true",
                        "M",
                        "1989-10-07",
                        "fr",
                        "fr",
                        "00",
                        "true"});
#line 18
 testRunner.And("create a resource owner", ((string)(null)), table2, "And ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Formatted",
                        "StreetAddress",
                        "Locality",
                        "Region",
                        "PostalCode",
                        "Country"});
            table3.AddRow(new string[] {
                        "formatted",
                        "streetaddress",
                        "locality",
                        "region",
                        "postalcode",
                        "country"});
#line 21
 testRunner.And("the following address is assigned to the resource owner", ((string)(null)), table3, "And ");
#line 24
 testRunner.And("authenticate the resource owner", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 25
 testRunner.And("the consent has been given by the resource owner habarthierry@loki.be for the cli" +
                    "ent MyHolidays and scopes openid,profile", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "scope",
                        "response_type",
                        "client_id",
                        "redirect_uri",
                        "prompt",
                        "state",
                        "nonce"});
            table4.AddRow(new string[] {
                        "openid profile",
                        "token id_token",
                        "MyHolidays",
                        "http://localhost",
                        "none",
                        "state1",
                        "parameterNonce"});
#line 26
 testRunner.And("requesting an access token", ((string)(null)), table4, "And ");
#line 30
 testRunner.When("requesting user information", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 32
 testRunner.Then("HTTP status code is 200", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 33
 testRunner.And("the claim sub with value habarthierry@loki.be is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
 testRunner.And("the claim name with value thabart is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
 testRunner.And("the claim family_name with value familyname is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 36
 testRunner.And("the claim given_name with value givename is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 37
 testRunner.And("the claim middle_name with value middlename is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 38
 testRunner.And("the claim nickname with value nickname is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 39
 testRunner.And("the claim preferred_username with value preferredusername is returned by the JWS " +
                    "payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
 testRunner.And("the claim profile with value profile is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
 testRunner.And("the claim picture with value picture is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 42
 testRunner.And("the claim website with value website is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
 testRunner.And("the claim gender with value M is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
 testRunner.And("the claim birthdate with value 1989-10-07 is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
 testRunner.And("the claim zoneinfo with value fr is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 46
 testRunner.And("the claim locale with value fr is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Fetch the user information for the scope address")]
        public virtual void FetchTheUserInformationForTheScopeAddress()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Fetch the user information for the scope address", ((string[])(null)));
#line 48
this.ScenarioSetup(scenarioInfo);
#line 49
 testRunner.Given("a mobile application MyHolidays is defined", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 50
 testRunner.And("set the name of the issuer http://localhost/identity", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 51
 testRunner.And("the redirection uri http://localhost is assigned to the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "IsInternal",
                        "Claims"});
            table5.AddRow(new string[] {
                        "openid",
                        "true",
                        ""});
            table5.AddRow(new string[] {
                        "address",
                        "true",
                        "address"});
#line 52
 testRunner.And("the scopes are defined", ((string)(null)), table5, "And ");
#line 56
 testRunner.And("the scopes openid,address are assigned to the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 57
 testRunner.And("the grant-type implicit is supported by the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 58
 testRunner.And("the response-types token,id_token are supported by the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Name"});
            table6.AddRow(new string[] {
                        "habarthierry@loki.be",
                        "thabart"});
#line 59
 testRunner.And("create a resource owner", ((string)(null)), table6, "And ");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Formatted",
                        "StreetAddress",
                        "Locality",
                        "Region",
                        "PostalCode",
                        "Country"});
            table7.AddRow(new string[] {
                        "formatted",
                        "streetaddress",
                        "locality",
                        "region",
                        "postalcode",
                        "country"});
#line 62
 testRunner.And("the following address is assigned to the resource owner", ((string)(null)), table7, "And ");
#line 65
 testRunner.And("authenticate the resource owner", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 66
 testRunner.And("the consent has been given by the resource owner habarthierry@loki.be for the cli" +
                    "ent MyHolidays and scopes openid,address", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "scope",
                        "response_type",
                        "client_id",
                        "redirect_uri",
                        "prompt",
                        "state",
                        "nonce"});
            table8.AddRow(new string[] {
                        "openid address",
                        "token id_token",
                        "MyHolidays",
                        "http://localhost",
                        "none",
                        "state1",
                        "parameterNonce"});
#line 67
 testRunner.And("requesting an access token", ((string)(null)), table8, "And ");
#line 71
 testRunner.When("requesting user information", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 73
 testRunner.Then("HTTP status code is 200", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 74
 testRunner.And("the claim sub with value habarthierry@loki.be is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Formatted",
                        "StreetAddress",
                        "Locality",
                        "Region",
                        "PostalCode",
                        "Country"});
            table9.AddRow(new string[] {
                        "formatted",
                        "streetaddress",
                        "locality",
                        "region",
                        "postalcode",
                        "country"});
#line 75
 testRunner.And("the returned address is", ((string)(null)), table9, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Fetch the user information for the scope email")]
        public virtual void FetchTheUserInformationForTheScopeEmail()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Fetch the user information for the scope email", ((string[])(null)));
#line 79
this.ScenarioSetup(scenarioInfo);
#line 80
 testRunner.Given("a mobile application MyHolidays is defined", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 81
 testRunner.And("set the name of the issuer http://localhost/identity", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 82
 testRunner.And("the redirection uri http://localhost is assigned to the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "IsInternal",
                        "Claims"});
            table10.AddRow(new string[] {
                        "openid",
                        "true",
                        ""});
            table10.AddRow(new string[] {
                        "email",
                        "true",
                        "email email_verified"});
#line 83
 testRunner.And("the scopes are defined", ((string)(null)), table10, "And ");
#line 87
 testRunner.And("the scopes openid,email are assigned to the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 88
 testRunner.And("the grant-type implicit is supported by the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 89
 testRunner.And("the response-types token,id_token are supported by the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Name",
                        "Email",
                        "EmailVerified"});
            table11.AddRow(new string[] {
                        "habarthierry@loki.be",
                        "thabart",
                        "habarthierry@hotmail.fr",
                        "true"});
#line 90
 testRunner.And("create a resource owner", ((string)(null)), table11, "And ");
#line 93
 testRunner.And("authenticate the resource owner", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 94
 testRunner.And("the consent has been given by the resource owner habarthierry@loki.be for the cli" +
                    "ent MyHolidays and scopes openid,email", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                        "scope",
                        "response_type",
                        "client_id",
                        "redirect_uri",
                        "prompt",
                        "state",
                        "nonce"});
            table12.AddRow(new string[] {
                        "openid email",
                        "token id_token",
                        "MyHolidays",
                        "http://localhost",
                        "none",
                        "state1",
                        "parameterNonce"});
#line 95
 testRunner.And("requesting an access token", ((string)(null)), table12, "And ");
#line 99
 testRunner.When("requesting user information", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 101
 testRunner.Then("HTTP status code is 200", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 102
 testRunner.And("the claim sub with value habarthierry@loki.be is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 103
 testRunner.And("the claim email with value habarthierry@hotmail.fr is returned by the JWS payload" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 104
 testRunner.And("the claim email_verified with value True is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Fetch the user information for the scope phone")]
        public virtual void FetchTheUserInformationForTheScopePhone()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Fetch the user information for the scope phone", ((string[])(null)));
#line 106
this.ScenarioSetup(scenarioInfo);
#line 107
 testRunner.Given("a mobile application MyHolidays is defined", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 108
 testRunner.And("set the name of the issuer http://localhost/identity", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 109
 testRunner.And("the redirection uri http://localhost is assigned to the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "IsInternal",
                        "Claims"});
            table13.AddRow(new string[] {
                        "openid",
                        "true",
                        ""});
            table13.AddRow(new string[] {
                        "phone",
                        "true",
                        "phone_number phone_number_verified"});
#line 110
 testRunner.And("the scopes are defined", ((string)(null)), table13, "And ");
#line 114
 testRunner.And("the scopes openid,phone are assigned to the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 115
 testRunner.And("the grant-type implicit is supported by the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 116
 testRunner.And("the response-types token,id_token are supported by the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Name",
                        "PhoneNumber",
                        "PhoneNumberVerified"});
            table14.AddRow(new string[] {
                        "habarthierry@loki.be",
                        "thabart",
                        "007",
                        "false"});
#line 117
 testRunner.And("create a resource owner", ((string)(null)), table14, "And ");
#line 120
 testRunner.And("authenticate the resource owner", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 121
 testRunner.And("the consent has been given by the resource owner habarthierry@loki.be for the cli" +
                    "ent MyHolidays and scopes openid,phone", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table15 = new TechTalk.SpecFlow.Table(new string[] {
                        "scope",
                        "response_type",
                        "client_id",
                        "redirect_uri",
                        "prompt",
                        "state",
                        "nonce"});
            table15.AddRow(new string[] {
                        "openid phone",
                        "token id_token",
                        "MyHolidays",
                        "http://localhost",
                        "none",
                        "state1",
                        "parameterNonce"});
#line 122
 testRunner.And("requesting an access token", ((string)(null)), table15, "And ");
#line 126
 testRunner.When("requesting user information", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 128
 testRunner.Then("HTTP status code is 200", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 129
 testRunner.And("the claim sub with value habarthierry@loki.be is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 130
 testRunner.And("the claim phone_number with value 007 is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 131
 testRunner.And("the claim phone_number_verified with value False is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Fetch the user information for the scope phone and email")]
        public virtual void FetchTheUserInformationForTheScopePhoneAndEmail()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Fetch the user information for the scope phone and email", ((string[])(null)));
#line 133
this.ScenarioSetup(scenarioInfo);
#line 134
 testRunner.Given("a mobile application MyHolidays is defined", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 135
 testRunner.And("set the name of the issuer http://localhost/identity", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 136
 testRunner.And("the redirection uri http://localhost is assigned to the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table16 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "IsInternal",
                        "Claims"});
            table16.AddRow(new string[] {
                        "openid",
                        "true",
                        ""});
            table16.AddRow(new string[] {
                        "phone",
                        "true",
                        "phone_number phone_number_verified"});
            table16.AddRow(new string[] {
                        "email",
                        "true",
                        "email email_verified"});
#line 137
 testRunner.And("the scopes are defined", ((string)(null)), table16, "And ");
#line 142
 testRunner.And("the scopes openid,phone,email are assigned to the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 143
 testRunner.And("the grant-type implicit is supported by the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 144
 testRunner.And("the response-types token,id_token are supported by the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table17 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Name",
                        "PhoneNumber",
                        "PhoneNumberVerified",
                        "Email",
                        "EmailVerified"});
            table17.AddRow(new string[] {
                        "habarthierry@loki.be",
                        "thabart",
                        "007",
                        "false",
                        "habarthierry@hotmail.fr",
                        "true"});
#line 145
 testRunner.And("create a resource owner", ((string)(null)), table17, "And ");
#line 148
 testRunner.And("authenticate the resource owner", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 149
 testRunner.And("the consent has been given by the resource owner habarthierry@loki.be for the cli" +
                    "ent MyHolidays and scopes openid,phone,email", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table18 = new TechTalk.SpecFlow.Table(new string[] {
                        "scope",
                        "response_type",
                        "client_id",
                        "redirect_uri",
                        "prompt",
                        "state",
                        "nonce"});
            table18.AddRow(new string[] {
                        "openid phone email",
                        "token id_token",
                        "MyHolidays",
                        "http://localhost",
                        "none",
                        "state1",
                        "parameterNonce"});
#line 150
 testRunner.And("requesting an access token", ((string)(null)), table18, "And ");
#line 154
 testRunner.When("requesting user information", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 156
 testRunner.Then("HTTP status code is 200", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 157
 testRunner.And("the claim sub with value habarthierry@loki.be is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 158
 testRunner.And("the claim phone_number with value 007 is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 159
 testRunner.And("the claim phone_number_verified with value False is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 160
 testRunner.And("the claim email with value habarthierry@hotmail.fr is returned by the JWS payload" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 161
 testRunner.And("the claim email_verified with value True is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Fetch the user information for the claim email : {userinfo : { name: { essential " +
            ": \'true\' }}}")]
        public virtual void FetchTheUserInformationForTheClaimEmailUserinfoNameEssentialTrue()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Fetch the user information for the claim email : {userinfo : { name: { essential " +
                    ": \'true\' }}}", ((string[])(null)));
#line 164
this.ScenarioSetup(scenarioInfo);
#line 165
 testRunner.Given("a mobile application MyHolidays is defined", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 166
 testRunner.And("set the name of the issuer http://localhost/identity", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 167
 testRunner.And("the redirection uri http://localhost is assigned to the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table19 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "IsInternal",
                        "Claims"});
            table19.AddRow(new string[] {
                        "openid",
                        "true",
                        ""});
#line 168
 testRunner.And("the scopes are defined", ((string)(null)), table19, "And ");
#line 171
 testRunner.And("the scopes openid are assigned to the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 172
 testRunner.And("the grant-type implicit is supported by the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 173
 testRunner.And("the response-types token,id_token are supported by the client MyHolidays", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table20 = new TechTalk.SpecFlow.Table(new string[] {
                        "Id",
                        "Name",
                        "PhoneNumber",
                        "PhoneNumberVerified",
                        "Email",
                        "EmailVerified"});
            table20.AddRow(new string[] {
                        "habarthierry@loki.be",
                        "thabart",
                        "007",
                        "false",
                        "habarthierry@hotmail.fr",
                        "true"});
#line 174
 testRunner.And("create a resource owner", ((string)(null)), table20, "And ");
#line 177
 testRunner.And("authenticate the resource owner", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 178
 testRunner.And("the consent has been given by the resource owner habarthierry@loki.be for the cli" +
                    "ent MyHolidays and claims name", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table21 = new TechTalk.SpecFlow.Table(new string[] {
                        "scope",
                        "response_type",
                        "client_id",
                        "redirect_uri",
                        "prompt",
                        "state",
                        "nonce",
                        "claims"});
            table21.AddRow(new string[] {
                        "openid",
                        "token id_token",
                        "MyHolidays",
                        "http://localhost",
                        "none",
                        "state1",
                        "parameterNonce",
                        "%7B%22userinfo%22%3A+%7B%22name%22%3A+%7B%22essential%22%3A+true%7D%7D%7D"});
#line 179
 testRunner.And("requesting an access token", ((string)(null)), table21, "And ");
#line 183
 testRunner.When("requesting user information", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 185
 testRunner.Then("HTTP status code is 200", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 186
 testRunner.And("the claim sub with value habarthierry@loki.be is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 187
 testRunner.And("the claim name with value thabart is returned by the JWS payload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
