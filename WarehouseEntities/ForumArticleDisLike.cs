using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Core.Entities;

namespace Warehouse.Entities
{
    [Table("ForumArticleDisLike")]
    public class ForumArticleDisLike : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ArticleId { get; set; }

        [StringLength(256)]
        public string UserName { get; set; }

        [ForeignKey("ArticleId")]
        public virtual ForumArticle ForumArticle { get; set; }
    }
}