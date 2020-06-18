using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Warehouse.Areas.Admin.Models
{
    public class CreateForumCategoryViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Bí danh")]
        public string Alias { get; set; }

        [Display(Name = "Số thứ tự")]
        public Nullable<int> SortOrder { get; set; }

        [Display(Name = "Hiển thị")]
        public bool Display { get; set; }
    }
}