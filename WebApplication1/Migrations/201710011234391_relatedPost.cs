namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relatedPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "relatedPost", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "relatedPost");
        }
    }
}
