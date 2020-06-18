using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Core.Entities;

namespace Warehouse.Entities
{
    public class ForumCategory: IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Bí danh")]
        public string Alias { get; set; }

        [Display(Name = "Mã Màu")]
        public string ColorCode { get; set; }

        [Display(Name = "Số thứ tự")]
        public Nullable<int> SortOrder { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Hiển thị")]
        public bool Display { get; set; }

        [Display(Name = "Ngày tạo")]
        public Nullable<DateTime> DateCreated { get; set; }

        [Display(Name = "Người dùng tạo")]
        [StringLength(256)]
        public string UserCreated { get; set; }

        public virtual ICollection<ForumArticle> ForumArticles { get; set; }

    }
}