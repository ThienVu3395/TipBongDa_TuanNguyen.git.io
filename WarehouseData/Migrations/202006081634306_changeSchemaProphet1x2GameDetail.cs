namespace Warehouse.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSchemaProphet1x2GameDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prophet1x2GameDetail", "match_hometeam_score", c => c.Int());
            AddColumn("dbo.Prophet1x2GameDetail", "match_awayteam_score", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prophet1x2GameDetail", "match_awayteam_score");
            DropColumn("dbo.Prophet1x2GameDetail", "match_hometeam_score");
        }
    }
}
