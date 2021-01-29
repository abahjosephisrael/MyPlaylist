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

            /// <summary>
            /// This method is the implementation of IRequestHandler
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<IEnumerable<VideoDTO>> Handle(VideoSearch request, CancellationToken cancellationToken)
            {
                List<VideoDTO> videos = new List<VideoDTO>();
                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = "",//The key you got from developer console.
                    ApplicationName = this.GetType().ToString()
                });

                var searchListRequest = youtubeService.Search.List("snippet");
                searchListRequest.Q = request.query;
                searchListRequest.MaxResults = 10;

                try
                {

                    //Call the search.list method to retrieve results matching the specified query term.
                     var searchListResponse = await searchListRequest.ExecuteAsync();

                    //Adding videos to list
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
                    //Returning the search result
                    return videos;

                }
                catch (Exception)
                {

                    return null;
                }                
            }
        }


    }
}
