using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Windows;
using System.Net;
using System.Configuration;

namespace PACDesktopClient.Connections
{
    class Authorization
    {
        public static string userToken = string.Empty;
        public static string usersEmail = string.Empty;

        public static void RegisterUser(
            string userName, string userPassword, string userConfirmPassword)
        {
            RestClient client = new RestClient(ConfigurationManager.AppSettings["hostHTTP"]);
            RestRequest request = new RestRequest("/api/Account/Register", Method.POST);
            
            var registerDictionary = new Dictionary<string, string>
                {
                    { "Email", userName },
                    { "Password", userPassword },
                    { "ConfirmPassword", userConfirmPassword }
                };

            string jsonRegisterData =
                JsonConvert.SerializeObject(registerDictionary);

            request.AddParameter("text/json", jsonRegisterData, ParameterType.RequestBody);
            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show("Poprawnie dodano użytkownika " + userName + "!");                
            }
            else
            {
                MessageBox.Show("Nie można dodać użytkownika " + userName + ".\r\n\r\n" +
                    "Kod błędu: " + response.StatusCode + "\r\nTreśc komunikatu błędu: " +
                    response.Content);
            }
        }

        public static string GetToken(string userName, string password)
        {
            HttpClient client = new HttpClient();
            var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>( "grant_type", "password" ),
                        new KeyValuePair<string, string>( "username", userName ),
                        new KeyValuePair<string, string> ( "Password", password )
                    };

            var content = new FormUrlEncodedContent(pairs);
            HttpResponseMessage response =
                client.PostAsync(ConfigurationManager.AppSettings["hostHTTP"] + "Token", content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            Dictionary<string, string> tokenDictionary =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
            string gottenToken = "";

            try
            {
                gottenToken = tokenDictionary["access_token"];
            }
            catch (Exception)
            {
                Debug.WriteLine("Token nie został zlokalizowany w odpowiedzi...");
            }
            return gottenToken;
        }                
    }
}
