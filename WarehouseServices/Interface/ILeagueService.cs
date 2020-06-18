using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Entities;

namespace Warehouse.Services.Interface
{
    public interface ILeagueService
    {
        List<League> GetLeaguesByCountryID(int CountryID);
        League GetLeagueByIdFromAPI(int countryID, int league_id);
        League GetLeagueByIdFromDatabase(int league_id);
        League GetLeague(int countryID, int league_id);
        void AddOrUpdate(League league);
    }
}
