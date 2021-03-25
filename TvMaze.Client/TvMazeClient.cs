using RestSharp;
using System.Collections.Generic;
using TvMaze.Client.Models;
using TvMaze.Client.Results;

namespace TvMazeScraper
{
    public class TvMazeClient
    {
        private readonly RestClient _client = new RestClient("https://api.tvmaze.com");

        public List<SearchResult> ShowSearch(string query)
        {
            var request = new RestRequest("search/shows");
            request.AddParameter("q", query);
            
            var response = _client.Get<List<SearchResult>>(request);
            if (!response.IsSuccessful)
                return null;

            return response.Data;
        }

        public Show GetShow(int id)
        {
            var request = new RestRequest("shows/{id}");
            request.AddParameter("id", id, ParameterType.UrlSegment);

            var response = _client.Get<Show>(request);
            if (!response.IsSuccessful)
                return null;

            return response.Data;
        }

        public List<Person>
    }
}
