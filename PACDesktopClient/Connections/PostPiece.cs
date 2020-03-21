using RestSharp;
using PACDesktopClient.Models;
using System.Windows;
using Newtonsoft.Json;

namespace PACDesktopClient.Connections
{
    class PostPiece
    {
        public static void PostThePiece(PieceOfArt poa)
        {
            var client = new RestClient("https://localhost:44385");
            var json = JsonConvert.SerializeObject(poa);
            var request = new RestRequest("/api/pieceofarts/", Method.POST)
            {
                RequestFormat = RestSharp.DataFormat.Json
            };
            request.AddHeader("Authorization", string.Format("bearer {0}", Authorization.userToken));
            request.AddHeader("Accept", "application/json");

            request.AddParameter("text/json", json, ParameterType.RequestBody);
            var response = client.Execute(request);
        }
    }
}
