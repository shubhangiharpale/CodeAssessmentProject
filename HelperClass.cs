using CodeAssessmentProject.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace CodeAssessmentProject
{
    class HelperClass<T>
    {


        public LoginResponse GetToken(string endpoint, dynamic payload, string guid)
        {
            var user = new WrapperClass<LoginRequest>();
            var url = user.SetUrlForLogin(endpoint);
            var json = user.serialize(payload);
            var req = user.LoginReuestToGetToken(json, guid);
            var response = user.GetResponse(url, req);
            LoginResponse content = user.GetContent<LoginResponse>(response);
            return content;

        }
        public GamePlayResponse GetBalance(string gameplayendpoint, dynamic payload, string productID, String ModuleID, string token)
        {
            var user = new WrapperClass<GamePlayRequest>();
            var url = user.SetUrlForLogin(gameplayendpoint);
            var json = user.serialize(payload);
            var req = user.GamePlayPostRequest(json, productID, ModuleID, token);
            var responce = user.GetResponse(url, req);
            GamePlayResponse content = user.GetContent<GamePlayResponse>(responce);
            return content;

        }


    }
}
