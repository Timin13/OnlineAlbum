namespace OnlineAlbum.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using OnlineAlbum.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DatabaseContext db)
        {
            UserProfile Admin = new UserProfile()
            {
                UserName = "Admin"
            };

            db.UserProfiles.Add(Admin);
            db.SaveChanges();
        }
    }
}
