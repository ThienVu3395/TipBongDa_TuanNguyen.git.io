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
    [Table("PlayGoldenGoalGame")]
    public partial class PlayGoldenGoalGame : IEntity
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        public string IP { get; set; }

        public int GoldenGoalGameId { get; set; }

        [Display(Name = "Mã cầu thủ ghi bàn")]
        public long player_id_result { get; set; }

        [Display(Name = "Tên cầu thủ ghi bàn")]
        public string player_name_result { get; set; }

        [Display(Name = "Số phút đầu tiên ghi bàn")]
        public string minute_goalscorer { get; set; }

        [Display(Name = "Ngày tham gia")]

        public DateTime CreatedDate { get; set; }

        [Display(Name = "Ngày sửa đổi cuối")]
        public DateTime LastChanged { get; set; }

        public virtual GoldenGoalGame GoldenGoalGame { get; set; }
    }
}
