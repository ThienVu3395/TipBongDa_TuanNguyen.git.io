using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Core.Entities;

namespace Warehouse.Entities
{
    public partial class Article: IEntity
    {
        public int Id { get; set; }

        [StringLength(256, ErrorMessage = "{0} không được vượt quá {1} ký tự!")]
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [NotMapped]
        public string ShortTitle
        {
            get
            {
                return Title.Length > 100 ? Title.Substring(0, 100) + "..." : Title;
            }
        }

        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

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
        public Nullable<bool> Display { get; set; }

        [StringLength(256, ErrorMessage = "{0} không được vượt quá {1} ký tự!")]
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Bí danh")]
        public string Alias { get; set; }

        [Display(Name = "Ngày tạo")]
        public Nullable<DateTime> DateCreated { get; set; }

        [Display(Name = "Tác giả")]
        [StringLength(256)]
        public string UserCreated { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Tin Nổi Bật")]
        public bool Hot { get; set; }

        [Display(Name = "Danh mục")]
        public int ArticleCategoryId { get; set; }

        public virtual ArticleCategory ArticleCategory { get; set; }
    }
}
