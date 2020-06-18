using System.Collections.Generic;
using Warehouse.Entities;
using Warehouse.Services.Interface;
using Warehouse.Data.Interface;
using System.Configuration;
using System.Linq;

namespace Warehouse.Services.Services
{
    public class Prophet1x2GameService : IProphet1x2GameService
    {
        readonly string APIKey = ConfigurationManager.AppSettings["APIKeyFootball"].ToString();

        private IProphet1x2GameDal _prophet1x2GameDal;

        public Prophet1x2GameService(IProphet1x2GameDal prophet1x2GameDal)
        {
            _prophet1x2GameDal = prophet1x2GameDal;
        }

        public void Active(Prophet1x2Game prophet1x2Game)
        {
            foreach(var item in _prophet1x2GameDal.GetList(x => x.Active == true))
            {
                item.Active = false;
                Update(item);
            }
            prophet1x2Game.Active = true;
            Update(prophet1x2Game);
        }

        public void Add(Prophet1x2Game prophet1x2Game)
        {
            _prophet1x2GameDal.Add(prophet1x2Game);
        }

        public void Awarded(int Id)
        {
            var prophet1x2Game = GetById(Id);
            prophet1x2Game.Awarded = true;
            Update(prophet1x2Game);
        }

        public void Delete(Prophet1x2Game prophet1x2Game)
        {
            _prophet1x2GameDal.Delete(prophet1x2Game);
        }

        public List<Prophet1x2Game> GetAll()
        {
            return _prophet1x2GameDal.GetList().OrderBy(x => x.Round).ToList();
        }

        public Prophet1x2Game GetProphet1x2GameActive()
        {
            return _prophet1x2GameDal.GetSingle(x => x.Active == true);
        }

        public Prophet1x2Game GetProphet1x2GameByRound(int Round)
        {
            return _prophet1x2GameDal.GetSingle(x => x.Round == Round);
        }

        public Prophet1x2Game GetById(int Id)
        {
            return _prophet1x2GameDal.GetSingle(x => x.Id == Id);
        }

        public void Update(Prophet1x2Game prophet1x2Game)
        {
            _prophet1x2GameDal.Update(prophet1x2Game);
        }
    }
}
