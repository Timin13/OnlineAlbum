using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace OnlineAlbum.Models
{
    public class ImageModel
    {
        [Key]
        public int ImageId { get; set; }

        [DataType(DataType.Text)]
        public string ImageDescription { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UploadDate { get; set; }
        public string ImagePath { get; set; }
        public int Rating { get; set; }

        public virtual UserProfile UserProfiles { get; set; }
    }
}