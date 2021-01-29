using Domain.DTOs;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Externals.YouTube
{
    public class VideoSearch : IRequest<IEnumerable<VideoDTO>>
    {
        public string query { get; set; }

        public class VideoSearchHandler : IRequestHandler<VideoSearch, IEnumerable<VideoDTO>>
        {
            public VideoSearchHandler()
            {

            }

            public async Task<IEnumerable<VideoDTO>> Handle(VideoSearch request, CancellationToken cancellationToken)
            {
                List<VideoDTO> videos = new List<VideoDTO>();
                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = "API_KEY",//The key you got from developer console.
                    ApplicationName = this.GetType().ToString()
                });

                var searchListRequest = youtubeService.Search.List("snippet");
                searchListRequest.Q = request.query;
                searchListRequest.MaxResults = 10;

                try
                {

                    //Call the search.list method to retrieve results matching the specified query term.
                     var searchListResponse = await searchListRequest.ExecuteAsync();

                    //Add each result to the appropriate list, and then display the lists of
                     //matching videos, channels, and playlists.
                    foreach (var searchResult in searchListResponse.Items)
                    {


                        if (searchResult.Id.Kind == "youtube#video")
                        {
                            var video = new VideoDTO()
                            {
                                Description = searchResult.Snippet.Description,
                                title = searchResult.Snippet.Title,
                                image = searchResult.Snippet.Thumbnails.Default__.Url
                            };
                            videos.Add(video);
                        }
                    }



                    return videos;

                    //WebRequest webrequest = WebRequest.Create("https://coderbyte.com/api/challenges/json/json-cleaning");
                    //WebResponse response = webrequest.GetResponse();
                    //// Info info = new Info();
                    //Stream newStream = response.GetResponseStream();
                    //StreamReader sr = new StreamReader(newStream);
                    ////var result = JsonSerializer.Deserialize<Info>(sr.ReadToEnd());
                    ////result.hobbies = new string[]{string.Join(",", result.hobbies.ToString().Remove(2, 1))};
                    ////var output = JsonSerializer.Serialize<Info>(result);
                    //response.Close();





                }
                catch (Exception)
                {

                    return null;
                }                
            }
        }


    }
}
