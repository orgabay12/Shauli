namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fan_country : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fans", "Country", c => c.String());
            DropColumn("dbo.Fans", "Seniority");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fans", "Seniority", c => c.Int(nullable: false));
            DropColumn("dbo.Fans", "Country");
        }
    }
}
