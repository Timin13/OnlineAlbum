using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;

namespace OnlineAlbum.Models
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserAvatar { get; set; }

        public virtual List<FriendList> FriendList { get; set; }
        public virtual List<ImageModel> Images { get; set; }
    }

    public class FriendList
    {
        public int Id { get; set; }
        public string FriendName { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    } 
}