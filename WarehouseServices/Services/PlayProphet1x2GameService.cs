using System.Collections.Generic;
using Warehouse.Entities;
using Warehouse.Services.Interface;
using Warehouse.Data.Interface;
using System.Configuration;
using System.Linq;

namespace Warehouse.Services.Services
{
    public class PlayProphet1x2GameService : IPlayProphet1x2GameService
    {
        readonly string APIKey = ConfigurationManager.AppSettings["APIKeyFootball"].ToString();

        private IPlayProphet1x2GameDal _playProphet1x2GameDal;
        private IProphet1x2GameDetailDal _prophet1x2GameDetailDal;

        public PlayProphet1x2GameService(IProphet1x2GameDetailDal prophet1x2GameDetailDal, IPlayProphet1x2GameDal playProphet1x2GameDal)
        {
            _playProphet1x2GameDal = playProphet1x2GameDal;
            _prophet1x2GameDetailDal = prophet1x2GameDetailDal;
        }

        public void Add(PlayProphet1x2Game playProphet1x2Game)
        {
            _playProphet1x2GameDal.Add(playProphet1x2Game);
        }

        public PlayProphet1x2Game GetById(int Id)
        {
            return _playProphet1x2GameDal.GetSingle(x => x.Id == Id);
        }

        public void Update(PlayProphet1x2Game playProphet1x2Game)
        {
            _playProphet1x2GameDal.Update(playProphet1x2Game);
        }

        public void Delete(PlayProphet1x2Game playProphet1x2Game)
        {
            _playProphet1x2GameDal.Delete(playProphet1x2Game);
        }

        public List<PlayProphet1x2Game> GetByUserName(string userName)
        {
            return _playProphet1x2GameDal.GetList(x => x.UserName == userName).OrderBy(x => x.Id).ToList();
        }
        public List<PlayProphet1x2Game> GetByProphet1x2GameId(int Prophet1x2GameId)
        {
            return _playProphet1x2GameDal.GetList(x => x.Prophet1x2GameDetail.Prophet1x2GameId == Prophet1x2GameId).OrderBy(x => x.Id).ToList();
        }

        public List<PlayProphet1x2Game> GetByProphet1x2GameDetailId(int Prophet1x2GameDetailId)
        {
            return _playProphet1x2GameDal.GetList(x => x.Prophet1x2GameDetailId == Prophet1x2GameDetailId).OrderBy(x => x.Id).ToList();
        }

        public void DeleteRangeByUserName(string userName)
        {
            List<PlayProphet1x2Game> playProphet1X2Games = GetByUserName(userName);
            foreach(PlayProphet1x2Game playProphet1X2Game in playProphet1X2Games)
            {
                Delete(playProphet1X2Game);
            }
        }

        public int CountAnswerCorrect(int Prophet1x2GameId)
        {
            List<PlayProphet1x2Game> playProphet1x2Games = GetByProphet1x2GameId(Prophet1x2GameId);
            int count = 0;
            foreach(PlayProphet1x2Game item in playProphet1x2Games)
            {
                if(item.Answer == item.Prophet1x2GameDetail.match_result)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
