using System.Collections.Generic;
using Warehouse.Entities;
using Warehouse.Services.Interface;
using Warehouse.Data.Interface;
using System.Configuration;
using Warehouse.Common;
using System.Linq;

namespace Warehouse.Services.Services
{
    public class GoldenGoalGameService : IGoldenGoalGameService
    {
        readonly string APIKey = ConfigurationManager.AppSettings["APIKeyFootball"].ToString();

        private IGoldenGoalGameDal _goldenGoalGameDal;

        public GoldenGoalGameService(IGoldenGoalGameDal goldenGoalGameDal)
        {
            _goldenGoalGameDal = goldenGoalGameDal;
        }

        public GoldenGoalGame GetGoldenGoalGameById(int Id)
        {
            return _goldenGoalGameDal.GetSingle(x => x.Id == Id);
        }

        public GoldenGoalGame GetGoldenGoalGameByRound(int Round)
        {
            return _goldenGoalGameDal.GetSingle(x => x.Round == Round);
        }

        public GoldenGoalGame GetGoldenGoalGameByMatchId(long match_id)
        {
            return _goldenGoalGameDal.GetSingle(x => x.match_id == match_id);
        }

        public void Add(GoldenGoalGame goldenGoalGame)
        {
            _goldenGoalGameDal.Add(goldenGoalGame);
        }

        public void Update(GoldenGoalGame goldenGoalGame)
        {
            _goldenGoalGameDal.Update(goldenGoalGame);
        }

        public List<GoldenGoalGame> GetAll()
        {
            return _goldenGoalGameDal.GetList();
        }

        public int GetMaxRound()
        {
            return _goldenGoalGameDal.Count() == 0 ? 1 : _goldenGoalGameDal.GetList().Max(x => x.Round);
        }

        public int GetNewRound()
        {
            return GetMaxRound() + 1;
        }

        public GoldenGoalGame GetGoldenGoalGameLastRound()
        {
            long maxRound = this.GetMaxRound();
            return _goldenGoalGameDal.GetSingle(x => x.Round == maxRound);
        }

        public bool CheckUserHavePlayed(int Id, string userName)
        {
            var goldenGoalGame = GetGoldenGoalGameById(Id);
            return goldenGoalGame.PlayGoldenGoalGames.Any(x => x.UserName == userName);
        }

        public bool CheckIPHavePlayed(int Id, string IP)
        {
            var goldenGoalGame = GetGoldenGoalGameById(Id);
            return goldenGoalGame.PlayGoldenGoalGames.Any(x => x.IP == IP);
        }

        public void Awarded(int Id)
        {
            var goldenGoalGame = GetGoldenGoalGameById(Id);
            goldenGoalGame.Awarded = true;
            Update(goldenGoalGame);
        }
    }
}
