namespace Warehouse.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterTableForumArticleLikeDisLikeV4 : DbMigration
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
               })
               .PrimaryKey(t => t.Id)
               .ForeignKey("dbo.ForumArticle", t => t.ArticleId)
               .Index(t => t.ArticleId);



            CreateTable(
                "dbo.ForumArticleLike",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ArticleId = c.Int(nullable: false),
                    UserName = c.String(maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ForumArticle", t => t.ArticleId)
                .Index(t => t.ArticleId);


        }
        
        public override void Down()
        {
         
        }
    }
}
