using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace MyPlaylistApi.Controllers
{
    /// <summary>
    /// This is the base controller of this application that other controllers inherits.
    /// The Mediator is the messenger/middle man between this controller and other controllers in this application.
    /// </summary>
    [Route("api/Playlist")]
    [ApiController]
    public class MainController : ControllerBase
    {
        //Field declaration.
        private IMediator _mediator;
        /// <summary>
        /// This is the entry point to all the controllers in  this application.
        /// It send http request through the mediator messenger to any controller that is invoked.
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
