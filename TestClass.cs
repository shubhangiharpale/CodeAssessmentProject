using CodeAssessmentProject.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Configuration;
using System.IO;

namespace CodeAssessmentProject
{
   
    public class Tests
    {
        
       
        public LoginResponse token;
         public string endpoints = "/v1/accounts/login/real";
        public string moduleId = ConfigurationManager.AppSettings["moduleId"];
        public  string productId = ConfigurationManager.AppSettings["productId"];


        [Test]
       
        public void LoginTestMethod()
        {
            LoginRequest data = JsonConvert.DeserializeObject<LoginRequest>(File.ReadAllText(@"C:\Users\Admin\source\repos\CodeAssessmentProject\TestData\LoginTestData.json"));

            LoginRequest loginRequest = new LoginRequest();
            
            var guid = ConfigurationManager.AppSettings["guid"];
          string payload = @"{""clientTypeId"": ""5"",
                               ""languageCode"": ""en""
                             }";
           //var request = login.LoginReuestToGetToken(guid, payload);

           // var loginRequest = new LoginRequest();
            loginRequest.UserName = data.UserName;
            loginRequest.Password = data.Password;
            loginRequest.SessionProductId = data.SessionProductId;
            loginRequest.ClientTypeId = data.ClientTypeId;
            loginRequest.LanguageCode = data.LanguageCode;
            loginRequest.NumLaunchTokens = data.NumLaunchTokens;
            loginRequest.MarketType = data.MarketType;

            
            var helper = new HelperClass<LoginResponse>();

            token = helper.GetToken( endpoints,loginRequest, guid);

            //var token1 = token.tokens.userToken;
            

            if (token!=null)
            {
                Console.WriteLine("Token generated successfully");
            }
            


        }
        [Test]
        public void GamePlayTestMethod()
        {
            GamePlayRequest data = JsonConvert.DeserializeObject<GamePlayRequest>(File.ReadAllText(@"C:\Users\Admin\source\repos\CodeAssessmentProject\TestData\GamePlayTestData.json"));
            var gameplaydata = new GamePlayRequest();

            string moduleId = ConfigurationManager.AppSettings["moduleId"];
            string productId = ConfigurationManager.AppSettings["productId"];
            gameplaydata.packetType = data.packetType;
            gameplaydata.payload = data.payload;
            gameplaydata.useFilter = data.useFilter;
            gameplaydata.isBase64Encoded = data.isBase64Encoded;

           

        var endpoint = "/v1/games/module/{{moduleID}}/client/{{clientID}}/play";
            var helper1 = new HelperClass<GamePlayResponse>();


            try
            {

                var generatedtoken = token.tokens.userToken;
                var gamebalance = helper1.GetBalance(endpoint, gameplaydata, productId, moduleId, generatedtoken);
                var financialbalance = gamebalance.context.financials.payoutAmount;
                var playerbalance = gamebalance.context.balances.totalInAccountCurrency;
                var balance = financialbalance + playerbalance;

                if (balance!= null)
                {
                    Console.WriteLine("Avaialble balance is:" + balance);
                }
            }
            catch(Exception e)
            {
               Console.WriteLine(e.StackTrace);
            }

            



        }
       
        }
    }
