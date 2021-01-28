using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Commands.PlayListCommands
{
    public class UpdatePlaylistCommand : PlaylistDTO, IRequest<Video>
    {
        public Guid VideoId { get; set; }
    public class UpdatePlaylistCommandHandler : IRequestHandler<UpdatePlaylistCommand, Video>
    {
        private readonly IApplicationDbContext context;

        public UpdatePlaylistCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        //This method handles the update command as inherited from IRequestHandler.
        public async Task<Video> Handle(UpdatePlaylistCommand request, CancellationToken cancellationToken)
        {

                //Getting the video object.
                var video = context.Playlists.FirstOrDefault(v => v.VideoId == request.VideoId);

                video.VideoName = request.VideoName;
                video.VideoThumbnail = request.VideoThumbnail;
                video.VideoUrl = request.VideoUrl;
            try
            {
                //Saving to database.
                context.Playlists.Update(video);
                await context.SaveChangesAsync();

                //Returning the updated video.
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
