using Application.Features.Commands.PlayListCommands;
using Application.Features.Queries.PlayListQueries;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MyPlaylistApi.Controllers
{
    /// <summary>
    /// This controller handles all crud of playlist
    /// </summary>
    public class VideoController : MainController
    {
        /// <summary>
        /// Contructor for dependency injections if any.
        /// </summary>
        public VideoController()
        {

        }


        /// <summary>
        /// This action method handles adding video to playlist
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Route("Video")]
        [HttpPost]
        public async Task<IActionResult> PlayList(PlaylistDTO command)
        {

            //Creating video object
            var video = new CreatePlaylistCommand()
            {
                VideoId = Guid.NewGuid(),
                VideoName = command.VideoName,
                VideoUrl = command.VideoUrl,
                VideoThumbnail = command.VideoThumbnail
            };

            //sending video to database.
            var response = await Mediator.Send(video);

            if (response == null)
            {
                //Returning error result
                return BadRequest(new { error = "An error occured while saving. Check fields and try again!" });
            }
            //Returning the created video
            return Ok(response);
        }


        /// <summary>
        /// This action method handles the update operation of video
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Route("Video")]
        [HttpPut]
        public async Task<IActionResult> PlayList(UpdatePlaylistCommand command)
        {
            if (command.VideoId == null)
            {
                //Returning error result
                return Ok(new { error = "Invalid video!" });
            }
            //sending video to database.
            var response = await Mediator.Send(command);

            if (response == null)
            {
                //Returning error result
                return Ok(new { error = "An error occured while updating the playlist. Check fields and try again!" });
            }
            //Returning the created video
            return Ok(response);
        }

        /// <summary>
        /// This endpoint handles the deletion of video from playlist
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Route("Video")]
        [HttpDelete]
        public async Task<IActionResult> PlayList(DeletePlaylistCommand command)
        {
            if (command.VideoId == null)
            {
                //Returning error result
                return BadRequest(new { error = "Invalid video!" });
            }
            //sending video to database.
            var response = await Mediator.Send(command);

            if (response == null)
            {
                //Returning error result
                return BadRequest(new { error = "An error occured while deleting video from the playlist. Check id and try again!" });
            }
            //Returning the created video
            return Ok(new { Message = "Video deleted successfully", VideoId = response });
        }

        /// <summary>
        /// This endpoint returns all videos in the play list.
        /// </summary>
        /// <returns></returns>
        [Route("GetAllVideos")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllVideosQuery()));
        }

        /// <summary>
        /// This endpoint returns a particular video whose id is parsed.
        /// </summary>
        /// <param name="VideoId"></param>
        /// <returns></returns>
        [HttpGet("{VideoId}")]
        public async Task<IActionResult> GetById(Guid VideoId)
        {
            return Ok(await Mediator.Send(new GetVideoByIdQuery { VideoId = VideoId }));
        }

    }
}
