using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Core.Entities;

namespace Warehouse.Entities
{
    public class ForumComment : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ForumArticleID { get; set; }

        public Nullable<int> ForumCommentParentID { get; set; }

        [StringLength(256)]
        public string UserComment { get; set; }

        [Display(Name = "Nội dung")]
        public string Content { get; set; }

        [Display(Name = "Hiển thị")]
        public bool Display { get; set; }

        [Display(Name = "Ngày tạo")]
        public Nullable<DateTime> DateComment { get; set; }

        [StringLength(256)]

        public string UserConfirmed { get; set; }

        public DateTime DateConfirmed { get; set; }

        [ForeignKey("ForumCommentParentID")]
        public virtual ForumComment ForumCommentParent { get; set; }

        public virtual ICollection<ForumComment> ForumChildComments { get; set; }

        [ForeignKey("ForumArticleID")]
        public virtual ForumArticle ForumArticle { get; set; }

    }
}