using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Core.Entities;

namespace Warehouse.Entities
{
    [Table("League")]
    public partial class League : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int country_id { get; set; }
        public string country_name { get; set; }
        public int league_id { get; set; }

        [Display(Name = "Tên giải đấu")]
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        public string league_name { get; set; }

        [Display(Name = "Số thứ tự")]
        public int? sort_order { get; set; }

        [Display(Name = "Logo")]
        public string logo { get; set; }

        [Display(Name = "Trạng thái")]
        public bool? status { get; set; }

        public bool? deleted { get; set; }
    }
}
