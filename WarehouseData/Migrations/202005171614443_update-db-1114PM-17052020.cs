namespace Warehouse.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb1114PM17052020 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PlayProphet1x2Game", "Answer", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PlayProphet1x2Game", "Answer", c => c.Long(nullable: false));
        }
    }
}
