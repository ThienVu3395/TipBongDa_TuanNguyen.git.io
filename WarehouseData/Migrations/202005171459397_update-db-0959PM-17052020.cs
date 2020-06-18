namespace Warehouse.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb0959PM17052020 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prophet1x2GameDetail", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.Prophet1x2GameDetail", "league_name", c => c.String());
            AlterColumn("dbo.Prophet1x2GameDetail", "match_hometeam_id", c => c.Long(nullable: false));
            AlterColumn("dbo.Prophet1x2GameDetail", "match_awayteam_id", c => c.Long(nullable: false));
            DropColumn("dbo.Prophet1x2GameDetail", "CreatedDated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prophet1x2GameDetail", "CreatedDated", c => c.DateTime());
            AlterColumn("dbo.Prophet1x2GameDetail", "match_awayteam_id", c => c.Int());
            AlterColumn("dbo.Prophet1x2GameDetail", "match_hometeam_id", c => c.Int());
            AlterColumn("dbo.Prophet1x2GameDetail", "league_name", c => c.Int(nullable: false));
            DropColumn("dbo.Prophet1x2GameDetail", "CreatedDate");
        }
    }
}
