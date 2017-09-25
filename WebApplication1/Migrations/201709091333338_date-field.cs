namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datefield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "PostDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Posts", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Posts", "PostDate");
        }
    }
}
