namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cancel_centers_key : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Centers");
            AlterColumn("dbo.Centers", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Centers", "Country", c => c.String());
            AddPrimaryKey("dbo.Centers", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Centers");
            AlterColumn("dbo.Centers", "Country", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Centers", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Centers", new[] { "ID", "Country" });
        }
    }
}
