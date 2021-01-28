using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Video
    {
        public Guid VideoId { get; set; }
        public Guid UserId { get; set; }
        public string VideoName { get; set; }
        public string VideoUrl { get; set; }
        public string VideoThumbnail { get; set; }
    }
}
