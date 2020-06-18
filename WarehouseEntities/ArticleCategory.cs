using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Core.Entities;

namespace Warehouse.Entities
{
    public partial class ArticleCategory : IEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        [Display(Name = "Bí danh")]
        public string Alias { get; set; }

        [Display(Name = "Số thứ tự")]
        public Nullable<int> SortOrder { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

    }
}
