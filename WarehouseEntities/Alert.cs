using System;
using System.ComponentModel.DataAnnotations;
using Warehouse.Core.Entities;

namespace Warehouse.Entities
{
    public partial class Alert : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [Display(Name = "Nội dung")]
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Ngày tạo")]
        public DateTime DateCreated { get; set; }

        [StringLength(256)]
        [Display(Name = "Tác giả")]
        public string Author { get; set; }

        [Display(Name = "Ngày hết hạn")]
        public Nullable<DateTime> ExpirationDate { get; set; }

    }
}
