using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyPlaylistMvc.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using MyPlaylistMvc.ViewModels;

namespace MyPlaylistMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Search( [FromQuery] string query)
        {

            List<VideoVM> videos = new List<VideoVM>();
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "",
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = query; 
            searchListRequest.MaxResults = 10;
            
            try
            {

                // Call the search.list method to retrieve results matching the specified query term.
                var searchListResponse = await searchListRequest.ExecuteAsync();

                // Add each result to the appropriate list, and then display the lists of
                // matching videos, channels, and playlists.
                foreach (var searchResult in searchListResponse.Items)
                {
                    

                    if (searchResult.Id.Kind == "youtube#video")
                    {
                        var video = new VideoVM()
                        {
                            Description = searchResult.Snippet.Description,
                            title = searchResult.Snippet.Title,
                            image = searchResult.Snippet.Thumbnails.Default__.Url
                        };
                        videos.Add(video);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            return View(videos);
        }
    }
}
