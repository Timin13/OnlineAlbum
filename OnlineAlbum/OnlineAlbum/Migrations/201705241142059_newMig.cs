namespace OnlineAlbum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newMig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageModels", "Content", c => c.Binary());
            DropColumn("dbo.ImageModels", "ImagePath");
            DropColumn("dbo.ImageModels", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ImageModels", "Rating", c => c.Int(nullable: false));
            AddColumn("dbo.ImageModels", "ImagePath", c => c.String());
            DropColumn("dbo.ImageModels", "Content");
        }
    }
}
