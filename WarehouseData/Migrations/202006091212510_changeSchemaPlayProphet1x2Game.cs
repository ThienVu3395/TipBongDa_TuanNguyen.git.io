namespace Warehouse.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSchemaPlayProphet1x2Game : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PlayProphet1x2Game", "IP");
            DropColumn("dbo.PlayProphet1x2Game", "LastChanged");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlayProphet1x2Game", "LastChanged", c => c.DateTime(nullable: false));
            AddColumn("dbo.PlayProphet1x2Game", "IP", c => c.String());
        }
    }
}
