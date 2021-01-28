using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Commands.PlayListCommands
{
    public class CreatePlaylistCommand : PlaylistDTO, IRequest<Video>
    {
        public Guid VideoId { get; set; }
        public class CreatePlaylistCommandHandler : IRequestHandler<CreatePlaylistCommand, Video>
        {
            private readonly IApplicationDbContext context;

            public CreatePlaylistCommandHandler(IApplicationDbContext context)
            {
                this.context = context;
            }

            //This method handles the create command as inherited from IRequestHandler.
            public async Task<Video> Handle(CreatePlaylistCommand request, CancellationToken cancellationToken)
            {
                //Creating the video object.
                var video = new Video()
                {
                    VideoId = request.VideoId,
                    VideoName = request.VideoName,
                    VideoThumbnail = request.VideoThumbnail,
                    VideoUrl = request.VideoUrl
                };

                try
                {
                    //Saving to database.
                    context.Playlists.Add(video);
                    await context.SaveChangesAsync();

                    //Returning the created video.
                    return video;
                }
                catch (Exception)
                {
                    //Returning null incase saving fails.
                    return null;
                }
            }
        }
    }
}
