using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Core.Entities;

namespace Warehouse.Entities
{
    [Table("Prophet1x2Game")]
    public class Prophet1x2Game : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Vòng đấu")]
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        public int Round { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDated { get; set; }

        [Display(Name = "Thành viên tạo")]
        [StringLength(256)]
        public string CreatedUser { get; set; }

        [DefaultValue(false)]
        [Display(Name = "Kích hoạt")]
        public bool Active { get; set; }

        [DefaultValue(true)]
        [Display(Name = "Trao thưởng")]
        public bool Awarded { get; set; }

        public virtual ICollection<Prophet1x2GameDetail> Prophet1x2GameDetails { get; set; }

    }
}
