using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Core.Entities;

namespace Warehouse.Entities
{
    [Table("ForumArticle")]
    public class ForumArticle : IEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(512, ErrorMessage = "{0} không được vượt quá {1} ký tự!")]
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [NotMapped]
        public string ShortDescription
        {
            get
            {
                return Description.Length > 100 ? Description.Substring(0, 100) + "..." : Description;
            }
        }

        [Display(Name = "Nội dung")]
        public string Content { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Hiển thị")]
        public bool Display { get; set; }

        [StringLength(512, ErrorMessage = "{0} không được vượt quá {1} ký tự!")]
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Bí danh")]
        public string Alias { get; set; }

        [Display(Name = "Ngày tạo")]
        public Nullable<DateTime> DateCreated { get; set; }

        [Display(Name = "Tác giả")]
        [StringLength(256)]
        public string UserCreated { get; set; }

        [Display(Name = "Người duyệt")]
        [StringLength(256)]
        public string UserConfirmed { get; set; }

        [Display(Name = "Ngày duyệt")]
        public Nullable<DateTime> DateConfirmed { get; set; }

        [Display(Name = "Lượt xem")]
        [DefaultValue(0)]
        public int Views { get; set; }

        [Display(Name = "Lượt thích")]
        [DefaultValue(0)]
        public int Likes { get; set; }

        [Display(Name = "Lượt không thích")]
        [DefaultValue(0)]
        public int Dislikes { get; set; }

        [Display(Name = "Danh mục")]
        public int ForumCategoryId { get; set; }

        public virtual ForumCategory ForumCategory { get; set; }

        public virtual ICollection<ForumComment> ForumComments { get; set; }

        public virtual ICollection<ForumArticleLike> ForumArticleLikes { get; set; }

        public virtual ICollection<ForumArticleDisLike> ForumArticleDisLikes { get; set; }

    }
}