namespace Warehouse.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createTableBlogandHandbook : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        Image = c.String(),
                        Content = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        LastModified = c.DateTime(),
                        Display = c.Boolean(),
                        Alias = c.String(),
                        Author = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Handbooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        Image = c.String(),
                        Content = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        LastModified = c.DateTime(),
                        Display = c.Boolean(),
                        Alias = c.String(),
                        Author = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Handbooks");
            DropTable("dbo.Blogs");
        }
    }
}
