namespace OnlineAlbum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newMigration : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.FriendLists", new[] { "UserId" });
            RenameColumn(table: "dbo.FriendLists", name: "UserId", newName: "UserProfile_UserId");
            DropPrimaryKey("dbo.FriendLists");
            AddColumn("dbo.FriendLists", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.FriendLists", "FriendName", c => c.String());
            AlterColumn("dbo.FriendLists", "UserProfile_UserId", c => c.Int());
            AddPrimaryKey("dbo.FriendLists", "Id");
            CreateIndex("dbo.FriendLists", "UserProfile_UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.FriendLists", new[] { "UserProfile_UserId" });
            DropPrimaryKey("dbo.FriendLists");
            AlterColumn("dbo.FriendLists", "UserProfile_UserId", c => c.Int(nullable: false));
            DropColumn("dbo.FriendLists", "FriendName");
            DropColumn("dbo.FriendLists", "Id");
            AddPrimaryKey("dbo.FriendLists", "UserId");
            RenameColumn(table: "dbo.FriendLists", name: "UserProfile_UserId", newName: "UserId");
            CreateIndex("dbo.FriendLists", "UserId");
        }
    }
}
