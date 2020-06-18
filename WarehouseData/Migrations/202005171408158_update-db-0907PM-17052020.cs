namespace Warehouse.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb0907PM17052020 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlayProphet1x2Game",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        IP = c.String(),
                        Prophet1x2GameDetailId = c.Int(nullable: false),
                        Answer = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastChanged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Prophet1x2GameDetail", t => t.Prophet1x2GameDetailId, cascadeDelete: true)
                .Index(t => t.Prophet1x2GameDetailId);
            
            AddColumn("dbo.Prophet1x2Game", "Awarded", c => c.Boolean(nullable: false));
            AddColumn("dbo.InfoWeb", "Prophet1x2GameRule", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayProphet1x2Game", "Prophet1x2GameDetailId", "dbo.Prophet1x2GameDetail");
            DropIndex("dbo.PlayProphet1x2Game", new[] { "Prophet1x2GameDetailId" });
            DropColumn("dbo.InfoWeb", "Prophet1x2GameRule");
            DropColumn("dbo.Prophet1x2Game", "Awarded");
            DropTable("dbo.PlayProphet1x2Game");
        }
    }
}
