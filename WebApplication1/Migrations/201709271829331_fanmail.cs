namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fanmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "AuthorEmail", c => c.String());
            DropColumn("dbo.Comments", "AuthorWebsite");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "AuthorWebsite", c => c.String());
            DropColumn("dbo.Comments", "AuthorEmail");
        }
    }
}
