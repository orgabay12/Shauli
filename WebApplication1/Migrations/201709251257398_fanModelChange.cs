namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fanModelChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fans", "LastName", c => c.String());
            DropColumn("dbo.Fans", "LastTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fans", "LastTime", c => c.String());
            DropColumn("dbo.Fans", "LastName");
        }
    }
}
