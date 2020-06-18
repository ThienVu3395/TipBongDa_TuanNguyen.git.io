namespace Warehouse.Data.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using global::Warehouse.Entities;

    public partial class WarehouseContext : DbContext
    {
        public WarehouseContext()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<MailBox> MailBoxes { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<Subscriber> Subscribers { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Handbook> Handbooks { get; set; } 
        public virtual DbSet<ArticleCategory> ArticleCategories { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<League> Leagues { get; set; }
        public virtual DbSet<GoldenGoalGame> GoldenGoalGames { get; set; }
        public virtual DbSet<PlayGoldenGoalGame> PlayGoldenGoalGames { get; set; }
        public virtual DbSet<ForumCategory> ForumCategories { get; set; }
        public virtual DbSet<ForumArticle> ForumArticles { get; set; }
        public virtual DbSet<ForumComment> ForumComments { get; set; }
        public virtual DbSet<ForumArticleLike> ForumArticleLikes { get; set; }
        public virtual DbSet<ForumArticleDisLike> ForumArticleDisLikes { get; set; }
        public virtual DbSet<Prophet1x2Game> Prophet1x2Games { get; set; }
        public virtual DbSet<Prophet1x2GameDetail> Prophet1x2GameDetails { get; set; }
        public virtual DbSet<PlayProphet1x2Game> PlayProphet1x2Games { get; set; }
        public virtual DbSet<Alert> Alerts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<District>()
                .HasMany(e => e.Wards)
                .WithRequired(e => e.District)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InfoWeb>()
                .Property(e => e.Logo)
                .IsUnicode(false);

            modelBuilder.Entity<InfoWeb>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<InfoWeb>()
                .Property(e => e.Zalo)
                .IsUnicode(false);

            modelBuilder.Entity<InfoWeb>()
                .Property(e => e.GoogleAnalytics)
                .IsUnicode(false);

            modelBuilder.Entity<Province>()
                .HasMany(e => e.Districts)
                .WithRequired(e => e.Province)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Slide>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Subscriber>()
                .Property(e => e.Email)
                .IsUnicode(false);


        }
    }
}
