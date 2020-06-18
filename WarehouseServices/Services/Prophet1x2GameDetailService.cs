using System.Collections.Generic;
using Warehouse.Entities;
using Warehouse.Services.Interface;
using Warehouse.Data.Interface;
using System.Configuration;
using System.Linq;

namespace Warehouse.Services.Services
{
    public class Prophet1x2GameDetailService : IProphet1x2GameDetailService
    {
        readonly string APIKey = ConfigurationManager.AppSettings["APIKeyFootball"].ToString();

        private IProphet1x2GameDetailDal _prophet1x2GameDetailDal;

        public Prophet1x2GameDetailService(IProphet1x2GameDetailDal prophet1x2GameDetailDal)
        {
            _prophet1x2GameDetailDal = prophet1x2GameDetailDal;
        }

        public void Add(Prophet1x2GameDetail prophet1x2GameDetail)
        {
            _prophet1x2GameDetailDal.Add(prophet1x2GameDetail);
        }

        public bool CheckUserHavePlayed(int Id, string userName)
        {
            return _prophet1x2GameDetailDal.GetFirst(x => x.CreatedUser == userName) != null;
        }

        public void Delete(Prophet1x2GameDetail prophet1x2GameDetail)
        {
            _prophet1x2GameDetailDal.Delete(prophet1x2GameDetail);
        }

        public List<Prophet1x2GameDetail> GetAll()
        {
            return _prophet1x2GameDetailDal.GetList();
        }

        public Prophet1x2GameDetail GetById(int Id)
        {
            return _prophet1x2GameDetailDal.GetSingle(x => x.Id == Id);
        }

        public List<Prophet1x2GameDetail> GetByProphet1x2GameId(int Prophet1x2GameId)
        {
            return _prophet1x2GameDetailDal.GetList(x => x.Prophet1x2GameId == Prophet1x2GameId).OrderBy(x => x.SortOrder).ToList();
        }

        public List<Prophet1x2GameDetail> GetByRound(int Round)
        {
            return _prophet1x2GameDetailDal.GetList(x => x.Prophet1x2Game.Round == Round).OrderBy(x => x.SortOrder).ToList();
        }

        public void Update(Prophet1x2GameDetail prophet1x2GameDetail)
        {
            _prophet1x2GameDetailDal.Update(prophet1x2GameDetail);
        }
    }
}
