namespace Warehouse.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeColumnCreatedDated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Prophet1x2Game", "CreatedDated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Prophet1x2Game", "CreatedDated", c => c.DateTime());
        }
    }
}
