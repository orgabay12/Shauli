namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _double : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Centers", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Centers", "Longitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Centers", "Longitude", c => c.Single(nullable: false));
            AlterColumn("dbo.Centers", "Latitude", c => c.Single(nullable: false));
        }
    }
}
