using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Warehouse.Core.DataAccess.EntityFramework;
using Warehouse.Data.Interface;
using Warehouse.Entities;
using System.Data.Entity;

namespace Warehouse.Data.Data
{
    public class Prophet1x2GameDetailDal : EntityRepositoryBase<Prophet1x2GameDetail, WarehouseContext>, IProphet1x2GameDetailDal
    {
        public override List<Prophet1x2GameDetail> GetList(Expression<Func<Prophet1x2GameDetail, bool>> filter = null)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<Prophet1x2GameDetail>().Include(x => x.Prophet1x2Game).ToList()
                    : context.Set<Prophet1x2GameDetail>().Include(x => x.Prophet1x2Game).Where(filter).ToList();
            }
        }
        public override Prophet1x2GameDetail GetSingle(Expression<Func<Prophet1x2GameDetail, bool>> filter)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<Prophet1x2GameDetail>().Include(x => x.Prophet1x2Game).SingleOrDefault()
                    : context.Set<Prophet1x2GameDetail>().Include(x => x.Prophet1x2Game).SingleOrDefault(filter);
            }
        }

        public override Prophet1x2GameDetail GetFirst(Expression<Func<Prophet1x2GameDetail, bool>> filter)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<Prophet1x2GameDetail>().Include(x => x.Prophet1x2Game).FirstOrDefault()
                    : context.Set<Prophet1x2GameDetail>().Include(x => x.Prophet1x2Game).FirstOrDefault(filter);
            }
        }
    }
}
