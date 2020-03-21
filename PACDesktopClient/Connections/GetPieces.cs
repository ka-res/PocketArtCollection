using System.Collections.Generic;
using RestSharp;
using PACDesktopClient.Models;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;
using System.Linq;

namespace PACDesktopClient.Connections
{
    class GetPieces
    {
        public static void GetAllPieces(UIElement uiElement)
        {
            RestClient restClient = new RestClient("https://localhost:44385");
            RestRequest restRequest = new RestRequest("api/pieceofarts/", Method.GET)
            {
                RequestFormat = RestSharp.DataFormat.Json
            };
            restRequest.AddHeader("Authorization", string.Format("bearer {0}", Authorization.userToken));
            restRequest.AddHeader("Accept", "application/json");

            List<PieceOfArt> queryResultAsCollection;

            if (Authorization.usersEmail == ConfigurationManager.AppSettings["adminEmail"])
            {
                queryResultAsCollection = restClient.Execute<List<PieceOfArt>>(restRequest).Data;
            }
            else
            {
                queryResultAsCollection = restClient.Execute<List<PieceOfArt>>(restRequest).Data
                    .Where(x => x.UsersEmail == Authorization.usersEmail).ToList();
            }

            (uiElement as ListView).ItemsSource = queryResultAsCollection;
        }
    }
}
