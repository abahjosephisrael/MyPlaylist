using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Commands.PlayListCommands
{
    public class DeletePlaylistCommand : IRequest<Guid>
    {
        public Guid VideoId { get; set; }
        public class DeletePlaylistCommandHandler : IRequestHandler<DeletePlaylistCommand, Guid>
        {
            private readonly IApplicationDbContext context;

            public DeletePlaylistCommandHandler(IApplicationDbContext context)
            {
                this.context = context;
            }

            public async Task<Guid> Handle(DeletePlaylistCommand request, CancellationToken cancellationToken)
            {
                //Getting the video from database.
                var video = context.Playlists.FirstOrDefault(v => v.VideoId == request.VideoId);
                //Checking if it exist
                if (video == null) return default;
                //Deleting from db
                context.Playlists.Remove(video);
                await context.SaveChangesAsync();
                //Returning the Id
                return video.VideoId;
            }
        }
    }
}
