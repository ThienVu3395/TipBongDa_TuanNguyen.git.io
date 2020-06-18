using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Core.Entities;

namespace Warehouse.Entities
{
    public partial class Blog: IEntity
    {
        public int Id { get; set; }

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
                return Description.Length > 200 ? Description.Substring(0, 200) + "..." : Description;
            }
        }

        [Display(Name = "Hình")]
        public string Image { get; set; }

        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Bạn chưa nhập {0}!")]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Ngày tạo")]
        public DateTime DateCreated { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Lần sửa cuối")]
        public Nullable<DateTime> LastModified { get; set; }

        [DefaultValue(true)]
        [Display(Name = "Hiển thị")]
        public Nullable<bool> Display { get; set; }

        [Display(Name = "Bí danh")]
        public string Alias { get; set; }

        [StringLength(256)]
        [Display(Name = "Tác giả")]
        public string Author { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Nổi bật")]
        public bool? IsHighlight { get; set; }
    }
}
