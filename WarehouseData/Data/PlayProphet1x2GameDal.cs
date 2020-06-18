using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Warehouse.Core.DataAccess.EntityFramework;
using Warehouse.Data.Interface;
using Warehouse.Entities;
using System.Data.Entity;

namespace Warehouse.Data.Data
{
    public class PlayProphet1x2GameDal : EntityRepositoryBase<PlayProphet1x2Game, WarehouseContext>, IPlayProphet1x2GameDal
    {
        public override List<PlayProphet1x2Game> GetList(Expression<Func<PlayProphet1x2Game, bool>> filter = null)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<PlayProphet1x2Game>().Include(x => x.Prophet1x2GameDetail).ToList()
                    : context.Set<PlayProphet1x2Game>().Include(x => x.Prophet1x2GameDetail).Where(filter).ToList();
            }
        }
        public override PlayProphet1x2Game GetSingle(Expression<Func<PlayProphet1x2Game, bool>> filter)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<PlayProphet1x2Game>().Include(x => x.Prophet1x2GameDetail).SingleOrDefault()
                    : context.Set<PlayProphet1x2Game>().Include(x => x.Prophet1x2GameDetail).SingleOrDefault(filter);
            }
        }

        public override PlayProphet1x2Game GetFirst(Expression<Func<PlayProphet1x2Game, bool>> filter)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<PlayProphet1x2Game>().Include(x => x.Prophet1x2GameDetail).FirstOrDefault()
                    : context.Set<PlayProphet1x2Game>().Include(x => x.Prophet1x2GameDetail).FirstOrDefault(filter);
            }
        }
    }
}
