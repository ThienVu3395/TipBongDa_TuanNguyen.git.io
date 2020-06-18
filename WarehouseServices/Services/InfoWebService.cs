using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Data.Interface;
using Warehouse.Entities;
using Warehouse.Services.Interface;

namespace Warehouse.Services.Services
{
    public class InfoWebService : IInfoWebService
    {
        readonly IInfoWebDal _infoWebDal;

        public InfoWebService(IInfoWebDal infoWebDal)
        {
            _infoWebDal = infoWebDal;
        }

        public void Update(InfoWeb infoWeb)
        {
            _infoWebDal.Update(infoWeb);
        }

        public InfoWeb GetFirst()
        {
            return _infoWebDal.GetFirst();
        }

        public List<InfoWeb> GetList()
        {
            return _infoWebDal.GetList();
        }
    }
}
