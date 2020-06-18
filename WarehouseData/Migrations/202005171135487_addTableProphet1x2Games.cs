namespace Warehouse.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableProphet1x2Games : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Prophet1x2GameDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Prophet1x2GameId = c.Int(nullable: false),
                        match_id = c.Int(),
                        league_id = c.Int(),
                        league_name = c.Int(nullable: false),
                        match_hometeam_id = c.Int(),
                        match_hometeam_name = c.String(),
                        match_hometeam_logo = c.String(),
                        match_awayteam_id = c.Int(),
                        match_awayteam_name = c.String(),
                        match_awayteam_logo = c.String(),
                        match_result = c.Int(),
                        SortOrder = c.Int(),
                        CreatedDated = c.DateTime(),
                        match_date = c.String(),
                        match_time = c.String(),
                        CreatedUser = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Prophet1x2Game", t => t.Prophet1x2GameId, cascadeDelete: true)
                .Index(t => t.Prophet1x2GameId);
            
            CreateTable(
                "dbo.Prophet1x2Game",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Round = c.Int(nullable: false),
                        CreatedDated = c.DateTime(),
                        CreatedUser = c.String(maxLength: 256),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prophet1x2GameDetail", "Prophet1x2GameId", "dbo.Prophet1x2Game");
            DropIndex("dbo.Prophet1x2GameDetail", new[] { "Prophet1x2GameId" });
            DropTable("dbo.Prophet1x2Game");
            DropTable("dbo.Prophet1x2GameDetail");
        }
    }
}
