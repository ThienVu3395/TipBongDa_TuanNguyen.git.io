using System;
using System.Collections.Generic;
using System.Linq;
using Warehouse.Data.Interface;
using Warehouse.Entities;
using Warehouse.Services.Interface;

namespace Warehouse.Services.Services
{
    public class AlertService : IAlertService
    {
        readonly IAlertDal _alertDal;

        public AlertService(IAlertDal alertDal)
        {
            _alertDal = alertDal;
        }

        public List<Alert> GetAll()
        {
            return _alertDal.GetList();
        }

        public Alert GetById(int Id)
        {
            return _alertDal.GetSingle(a => a.Id == Id);
        }

        public void Add(Alert alert)
        {
            _alertDal.Add(alert);
        }

        public void Update(Alert alert)
        {
            _alertDal.Update(alert);
        }

        public void Delete(int Id)
        {
            Alert alert = GetById(Id);
            if(alert != null)
                _alertDal.Delete(alert);
        }

        public List<Alert> GetToShow()
        {
            return _alertDal.GetList(x => (DateTime.Now < x.ExpirationDate || x.ExpirationDate == null)).OrderByDescending(x => x.Id).ToList();
        }

        public int CountListToShow()
        {
            return _alertDal.Count(x => (DateTime.Now < x.ExpirationDate || x.ExpirationDate == null));
        }
    }
}
