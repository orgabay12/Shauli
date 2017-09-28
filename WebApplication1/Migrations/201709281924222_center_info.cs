namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class center_info : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Centers", "Address", c => c.String());
            AddColumn("dbo.Centers", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Centers", "Phone");
            DropColumn("dbo.Centers", "Address");
        }
    }
}
