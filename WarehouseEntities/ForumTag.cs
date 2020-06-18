using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Entities
{
    public class ForumTag
    {
        [Key]
        public int Id { get; set; }

        public int ForumArticleID { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [ForeignKey("ForumArticleID")]
        public virtual ForumArticle ForumArticle { get; set; }
    }
}