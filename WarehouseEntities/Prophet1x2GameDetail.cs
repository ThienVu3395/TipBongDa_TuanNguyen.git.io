using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouse.Core.Entities;

namespace Warehouse.Entities
{
    [Table("Prophet1x2GameDetail")]
    public class Prophet1x2GameDetail : IEntity
    {
        public enum MatchResult{
            HomeWon = 1,
            Draw = 0,
            AwayWon = 2
        }
        public int Id { get; set; }
        public int Prophet1x2GameId { get; set; }
        public int? match_id { get; set; }
        public int? league_id { get; set; }
        public string league_name { get; set; }
        public long match_hometeam_id { get; set; }
        public string match_hometeam_name { get; set; }
        public string match_hometeam_logo { get; set; }
        public long match_awayteam_id { get; set; }
        public string match_awayteam_name { get; set; }
        public string match_awayteam_logo { get; set; }
        public int? match_hometeam_score { get; set; }
        public int? match_awayteam_score { get; set; }
        public int? match_result { get; set; }
        public int? SortOrder { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public string match_date { get; set; }
        public string match_time { get; set; }

        [StringLength(256)]
        public string CreatedUser { get; set; }

        [NotMapped]
        public bool IsDisabled
        {
            get
            {
                if (!string.IsNullOrEmpty(match_date) && !string.IsNullOrEmpty(match_time))
                {
                    DateTime dateTime = DateTime.ParseExact(match_date + " " + match_time, "dd-MM-yyyy HH:mm", null);
                    if ((DateTime.Now - dateTime).TotalHours <= 1)
                    {
                        return true;
                    }
                    else if (match_result != null || match_hometeam_score != null || match_awayteam_score != null)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                return true;
            }
        }

        public virtual Prophet1x2Game Prophet1x2Game { get; set; }
        public virtual ICollection<PlayProphet1x2Game> PlayProphet1x2Games { get; set; }

    }
}
