using System.Collections.Generic;
using Warehouse.Entities;
using Warehouse.Services.Interface;
using Warehouse.Data.Interface;
using System.Configuration;
using System.Linq;

namespace Warehouse.Services.Services
{
    public class PlayGoldenGoalGameService : IPlayGoldenGoalGameService
    {
        readonly string APIKey = ConfigurationManager.AppSettings["APIKeyFootball"].ToString();

        private IPlayGoldenGoalGameDal _playGoldenGoalGameDal;

        public PlayGoldenGoalGameService(IPlayGoldenGoalGameDal playGoldenGoalGameDal)
        {
            _playGoldenGoalGameDal = playGoldenGoalGameDal;
        }

        public void Add(PlayGoldenGoalGame playGoldenGoalGame)
        {
            _playGoldenGoalGameDal.Add(playGoldenGoalGame);
        }

        public List<PlayGoldenGoalGame> GetByGoldenGoalGameId(int goldenGoalGameId)
        {
            return _playGoldenGoalGameDal.GetList(x => x.GoldenGoalGameId == goldenGoalGameId).OrderByDescending(x => x.Id).ToList();
        }

        public PlayGoldenGoalGame GetById(int Id)
        {
            return _playGoldenGoalGameDal.GetSingle(x => x.Id == Id);
        }

        public void Update(PlayGoldenGoalGame playGoldenGoalGame)
        {
            _playGoldenGoalGameDal.Update(playGoldenGoalGame);
        }

        public void Delete(PlayGoldenGoalGame playGoldenGoalGame)
        {
            _playGoldenGoalGameDal.Delete(playGoldenGoalGame);
        }

        public List<PlayGoldenGoalGame> GetByUserName(string userName)
        {
            return _playGoldenGoalGameDal.GetList(x => x.UserName == userName).OrderByDescending(x => x.GoldenGoalGame.Round).ToList();
        }
    }
}
