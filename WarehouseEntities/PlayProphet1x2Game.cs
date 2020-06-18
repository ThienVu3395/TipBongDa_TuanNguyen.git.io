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
    [Table("PlayProphet1x2Game")]
    public partial class PlayProphet1x2Game : IEntity
    {
        public int Id { get; set; }
        
        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        public int Prophet1x2GameDetailId { get; set; }

        [Display(Name = "Dự đoán")]
        public int Answer { get; set; }

        [Display(Name = "Ngày dự đoán")]
        public DateTime CreatedDate { get; set; }

        public virtual Prophet1x2GameDetail Prophet1x2GameDetail { get; set; }
    }
}
