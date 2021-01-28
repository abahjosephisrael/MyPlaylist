using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Queries.PlayListQueries
{
    public class GetVideoByIdQuery : IRequest<Video>
    {
        public Guid VideoId { get; set; }

        public class GetVideoByIdQueryHandler : IRequestHandler<GetVideoByIdQuery, Video>
        {
            private readonly IApplicationDbContext context;

            public GetVideoByIdQueryHandler(IApplicationDbContext context)
            {
                this.context = context;
            }

            /// <summary>
            /// This returns the video whose id was parsed.
            /// </summary>
            /// <param name="query"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<Video> Handle(GetVideoByIdQuery query, CancellationToken cancellationToken)
            {
                //Getting the video by id
                var video =  context.Playlists.FirstOrDefault(v => v.VideoId == query.VideoId);
                if (video == null) return null;
                return video;
            }
        }
    }
}
