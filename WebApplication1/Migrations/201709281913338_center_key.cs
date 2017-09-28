namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class center_key : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Centers");
            AlterColumn("dbo.Centers", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Centers", "Country", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Centers", new[] { "ID", "Country" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Centers");
            AlterColumn("dbo.Centers", "Country", c => c.String());
            AlterColumn("dbo.Centers", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Centers", "ID");
        }
    }
}
