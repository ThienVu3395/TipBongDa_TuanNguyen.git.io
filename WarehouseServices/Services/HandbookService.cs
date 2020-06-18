using System.Collections.Generic;
using System.Linq;
using Warehouse.Data.Interface;
using Warehouse.Entities;
using Warehouse.Services.Interface;

namespace Warehouse.Services.Services
{
    public class HandbookService : IHandbookService
    {
        IHandbookDal _handbookDal;

        public HandbookService(IHandbookDal handbookDal)
        {
            _handbookDal = handbookDal;
        }

        public List<Handbook> GetAll()
        {
            return _handbookDal.GetList();
        }

        public List<Handbook> GetListByDisplay(bool Display)
        {
           return _handbookDal.GetList(b => b.Display == Display);
        }

        public Handbook GetByAlias(string alias, bool? display = null)
        {
            if (display == null)
                return _handbookDal.GetSingle(b => b.Alias == alias);
            else
                return _handbookDal.GetSingle(x => x.Alias == alias && x.Display == display.Value);
        }

        public Handbook GetById(int Id)
        {
             return _handbookDal.GetSingle(b => b.Id == Id);
        }

        public bool CheckUniqueTitle(string Title)
        {
            return _handbookDal.GetFirst(b => b.Title == Title) == null;
        }

        public bool CheckUniqueAlias(string Alias)
        {
            return _handbookDal.GetFirst(b => b.Alias == Alias) == null;
        }

        public void Add(Handbook Handbook)
        {
            _handbookDal.Add(Handbook);
        }

        public void Update(Handbook Handbook)
        {
            _handbookDal.Update(Handbook);
        }

        public void Delete(int Id)
        {
            Handbook Handbook = GetById(Id);
            if (Handbook != null)
                _handbookDal.Delete(Handbook);
        }
        
        public int CountDisplay()
        {
            return _handbookDal.Count(b => b.Display == true);
        }

        public int CountHide()
        {
            return _handbookDal.Count(b => b.Display == false);
        }

        public int CountByTitle(string Title)
        {
            return _handbookDal.Count(x => x.Title == Title);
        }

        public List<Handbook> GetListHighLight()
        {
            return _handbookDal.GetList(b => b.Display == true && b.IsHighlight == true).OrderByDescending(x => x.Id).ToList();
        }

        public List<Handbook> GetNewHandbooks(int take)
        {
            return _handbookDal.GetList(b => b.Display == true).OrderByDescending(x => x.Id).Take(take).ToList();
        }
    }
}
