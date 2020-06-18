using System.Collections.Generic;
using Warehouse.Entities;
using Warehouse.Services.Interface;
using Warehouse.Data.Interface;
using System.Configuration;
using Warehouse.Common;
using System.Linq;

namespace Warehouse.Services.Services
{
    public class LeagueService : ILeagueService
    {
        readonly string APIKey = ConfigurationManager.AppSettings["APIKeyFootball"].ToString();
        readonly string API_League = ConfigurationManager.AppSettings["API_League"].ToString();

        private ILeagueDal _leagueDal;

        public LeagueService(ILeagueDal leagueDal)
        {
            _leagueDal = leagueDal;
        }

        public List<League> GetLeaguesByCountryID(int countryID)
        {
            List<League> leagesFromAPI = API.GetListAPI<League>(API_League + "&country_id=" + countryID + "&APIkey=" + APIKey);
            List<League> leagueFromDatabase = _leagueDal.GetList(x => x.country_id == countryID);
            var leagues =
                from a in leagesFromAPI
                join b in leagueFromDatabase on a.league_id equals b.league_id into ps
                from p in ps.DefaultIfEmpty()
                select new League (){ 
                    Id = p.Id,
                    league_id = a.league_id, 
                    country_id = a.country_id, 
                    country_name = a.country_name, 
                    league_name = a.league_name,
                    logo = p?.logo ?? "no_image.jpg",
                    deleted = p?.deleted,
                    sort_order = p?.sort_order,
                    status = p?.status
                };
            return leagues.OrderBy(x => x.sort_order).ThenBy(x => x.league_id).ToList();
        }

        public League GetLeagueByIdFromAPI(int countryID, int league_id)
        {
            List<League> leagesFromAPI = API.GetListAPI<League>(API_League + "&country_id=" + countryID + "&APIkey=" + APIKey);
            return leagesFromAPI.FirstOrDefault(x => x.league_id == league_id);
        }

        public League GetLeagueByIdFromDatabase(int league_id)
        {
            return _leagueDal.GetSingle(x => x.league_id == league_id);
        }

        public void AddOrUpdate(League league)
        {
            if(GetLeagueByIdFromDatabase(league.league_id) == null)
            {
                _leagueDal.Add(league);
            }
            else 
                _leagueDal.Update(league);
        }

        public League GetLeague(int countryID, int league_id)
        {
            return GetLeaguesByCountryID(countryID).FirstOrDefault(x => x.league_id == league_id);
        }
    }
}
