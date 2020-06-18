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
    [Table("GoldenGoalGame")]
    public partial class GoldenGoalGame : IEntity
    {
        public int Id { get; set; }
        public int Round { get; set; }
        public long match_id { get; set; }
        public int league_id { get; set; }

        [Display(Name = "Tên giải đấu")]
        [Required(ErrorMessage = "Bạn chưa nhập {0}")]
        public string league_name { get; set; }
        public string match_date { get; set; }
        public string match_status { get; set; }
        public string match_time { get; set; }
        public long match_hometeam_id { get; set; }
        public string match_hometeam_name { get; set; }
        public string match_hometeam_logo { get; set; }
        public string match_hometeam_score { get; set; }
        public long match_awayteam_id { get; set; }
        public string match_awayteam_name { get; set; }
        public string match_awayteam_logo { get; set; }
        public string match_awayteam_score { get; set; }
        public long? player_id_result { get; set; }
        public string player_name_result { get; set; }
        public string minute_goalscorer { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }

        [DefaultValue(false)]
        public bool Awarded { get; set; }

        public virtual ICollection<PlayGoldenGoalGame> PlayGoldenGoalGames { get; set; }

    }
}
