namespace Warehouse.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumnIsHighlight : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "IsHighlight", c => c.Boolean());
            AddColumn("dbo.Handbooks", "IsHighlight", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Handbooks", "IsHighlight");
            DropColumn("dbo.Blogs", "IsHighlight");
        }
    }
}
