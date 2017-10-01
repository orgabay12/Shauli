namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uniquetitle : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Title", c => c.String(maxLength: 255));
            CreateIndex("dbo.Posts", "Title", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Posts", new[] { "Title" });
            AlterColumn("dbo.Posts", "Title", c => c.String());
        }
    }
}
