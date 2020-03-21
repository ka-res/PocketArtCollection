using Newtonsoft.Json;
using PACDesktopClient.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PACDesktopClient.Connections
{
    class PutPiece
    {
        public static void PutThePiece(PieceOfArt poa)
        {
            var client = new RestClient("https://localhost:44385");
            var json = JsonConvert.SerializeObject(poa);
            var request = new RestRequest("/api/pieceofarts/" + poa.Id, Method.PUT)
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
