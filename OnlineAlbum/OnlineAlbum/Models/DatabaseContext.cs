using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OnlineAlbum.Models
{
        public class DatabaseContext : DbContext
        {
            public DatabaseContext()
                : base("DefaultConnection")
            {

            }
            public DbSet<ImageModel> Images { get; set; }
            public DbSet<UserProfile> UserProfiles { get; set; }
            public DbSet<FriendList> FriendList { get; set; }
        }
}