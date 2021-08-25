using CodeAssessmentProject.DTO;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace CodeAssessmentProject
{
    public class WrapperClass<T>
    {
        
       
       public string hostname = ConfigurationManager.AppSettings["hostname"];
        //public string endpoints = "/v1/accounts/login/real";

        public RestClient SetUrlForLogin(string endpoints)
        {

            var url = $"{hostname}/{endpoints}";
            RestClient client = new RestClient(url);
            return client;
        }
        public RestRequest LoginReuestToGetToken(string guid, string payload)
        {
            //loginResponse = new LoginResponse();

            var restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("X-CorrelationId", guid);
            restRequest.AddHeader("X-Forwarded-For", guid);
            restRequest.AddHeader("X-Clienttypeid", "5");
            restRequest.AddParameter("application/json", payload, ParameterType.RequestBody);
          

            return restRequest;

        }
        public RestRequest GamePlayPostRequest(String payload, string productId, string moduleId, string bearerToken)
        {
            var restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("X-Route-ProductId", productId);
            restRequest.AddHeader("X-Route-ModuleId", moduleId);
            restRequest.AddHeader("X-Clienttypeid", "38");
            restRequest.AddParameter("Authorization", string.Format("Bearer " + bearerToken));
            restRequest.AddHeader("X - correlationid", "93D10259 - 30F8 - 4339 - B456 - 3F30A43F65A2");
            restRequest.AddParameter("application/json", payload, ParameterType.RequestBody);
            return restRequest;
        }
       
        public IRestResponse GetResponse(RestClient client, RestRequest request)
        {
            return client.Execute(request);
        }
        public DTO GetContent<DTO>(IRestResponse response)
        {
            var content = response.Content;
            DTO dtoObject = JsonConvert.DeserializeObject<DTO>(content);
            return dtoObject;
        }
        public string serialize(dynamic content)
        {
            string serializeObject = JsonConvert.SerializeObject(content, Formatting.Indented);
            return serializeObject;
        }

    }
}

