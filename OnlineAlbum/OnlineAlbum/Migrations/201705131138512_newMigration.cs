namespace OnlineAlbum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FriendName = c.String(),
                        UserProfile_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.UserProfile_UserId)
                .Index(t => t.UserProfile_UserId);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserAvatar = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.ImageModels",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImageDescription = c.String(),
                        UploadDate = c.DateTime(nullable: false),
                        ImagePath = c.String(),
                        Rating = c.Int(nullable: false),
                        UserProfiles_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.UserProfile", t => t.UserProfiles_UserId)
                .Index(t => t.UserProfiles_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImageModels", "UserProfiles_UserId", "dbo.UserProfile");
            DropForeignKey("dbo.FriendLists", "UserProfile_UserId", "dbo.UserProfile");
            DropIndex("dbo.ImageModels", new[] { "UserProfiles_UserId" });
            DropIndex("dbo.FriendLists", new[] { "UserProfile_UserId" });
            DropTable("dbo.ImageModels");
            DropTable("dbo.UserProfile");
            DropTable("dbo.FriendLists");
        }
    }
}
