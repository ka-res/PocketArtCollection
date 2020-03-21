using PACDesktopClient.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PACDesktopClient.Connections
{
    class DeletePiece
    {
        public static void RemoveThePiece(PieceOfArt poa)
        {
            var client = new RestClient("https://localhost:44385");
            var request = new RestRequest("api/pieceofarts/" + poa.Id, Method.DELETE)
            {
                RequestFormat = RestSharp.DataFormat.Json
            };
            request.AddBody(request.JsonSerializer.Serialize(poa));
            request.AddHeader("Authorization", string.Format("bearer {0}", Authorization.userToken));
            request.AddHeader("Accept", "application/json");

            IRestResponse response = client.Execute(request);
        }        
    }
}
