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
    public class Prophet1x2GameDal : EntityRepositoryBase<Prophet1x2Game, WarehouseContext>, IProphet1x2GameDal
    {
        public override List<Prophet1x2Game> GetList(Expression<Func<Prophet1x2Game, bool>> filter = null)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<Prophet1x2Game>().Include(x => x.Prophet1x2GameDetails).ToList()
                    : context.Set<Prophet1x2Game>().Include(x => x.Prophet1x2GameDetails).Where(filter).ToList();
            }
        }
        public override Prophet1x2Game GetSingle(Expression<Func<Prophet1x2Game, bool>> filter)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<Prophet1x2Game>().Include(x => x.Prophet1x2GameDetails).SingleOrDefault()
                    : context.Set<Prophet1x2Game>().Include(x => x.Prophet1x2GameDetails).SingleOrDefault(filter);
            }
        }

        public override Prophet1x2Game GetFirst(Expression<Func<Prophet1x2Game, bool>> filter)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<Prophet1x2Game>().Include(x => x.Prophet1x2GameDetails).FirstOrDefault()
                    : context.Set<Prophet1x2Game>().Include(x => x.Prophet1x2GameDetails).FirstOrDefault(filter);
            }
        }
    }
}
