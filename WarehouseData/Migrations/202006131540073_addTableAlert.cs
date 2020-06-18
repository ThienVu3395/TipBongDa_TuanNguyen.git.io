namespace Warehouse.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableAlert : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alerts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Author = c.String(maxLength: 256),
                        ExpirationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Alerts");
        }
    }
}
