using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Queries.PlayListQueries
{
    public class GetAllVideosQuery : IRequest<IEnumerable<Video>>
    {

        public class GetAllVideosQueryHandler : IRequestHandler<GetAllVideosQuery, IEnumerable<Video>>
        {
            private readonly IApplicationDbContext context;

            public GetAllVideosQueryHandler(IApplicationDbContext context)
            {
                this.context = context;
            }

            public async Task<IEnumerable<Video>> Handle(GetAllVideosQuery request, CancellationToken cancellationToken)
            {
                //Getting all videos
                var playList = await context.Playlists.ToListAsync();
                //Returns null when list is empty.
                if (playList == null) return null;
                //Returning playis
                return playList;
            }
        }
    }
}
