﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LKWSpringerApp.Web.ViewModels.ClientImage
{
    public class DetailsClientImageModel
    {
        public Guid ClientId { get; set; }
        public string ClientName { get; set; } = null!;
        public List<MediaFileModel> MediaFiles { get; set; } = new List<MediaFileModel>();
    }

    public class MediaFileModel
    {
        public Guid Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? Description { get; set; }
    }
}
