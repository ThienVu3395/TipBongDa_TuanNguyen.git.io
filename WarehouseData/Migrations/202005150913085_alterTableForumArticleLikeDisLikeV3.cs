namespace Warehouse.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterTableForumArticleLikeDisLikeV3 : DbMigration
    {
        public override void Up()
        {
            
            
            CreateTable(
                "dbo.ForumArticleDisLike",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                        UserName = c.String(maxLength: 256),
                        ForumArticle_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ForumArticle", t => t.ForumArticle_Id)
                .Index(t => t.ForumArticle_Id);
            
            
            
            CreateTable(
                "dbo.ForumArticleLike",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                        UserName = c.String(maxLength: 256),
                        ForumArticle_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ForumArticle", t => t.ForumArticle_Id)
                .Index(t => t.ForumArticle_Id);
            
            
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ForumArticleLike");
            DropTable("dbo.ForumArticleDisLike");
        }
    }
}
