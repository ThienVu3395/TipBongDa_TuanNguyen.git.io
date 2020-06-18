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
    public class PlayGoldenGoalGameDal : EntityRepositoryBase<PlayGoldenGoalGame, WarehouseContext>, IPlayGoldenGoalGameDal
    {
        public override List<PlayGoldenGoalGame> GetList(Expression<Func<PlayGoldenGoalGame, bool>> filter = null)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<PlayGoldenGoalGame>().Include(x => x.GoldenGoalGame).ToList()
                    : context.Set<PlayGoldenGoalGame>().Include(x => x.GoldenGoalGame).Where(filter).ToList();
            }
        }
        public override PlayGoldenGoalGame GetSingle(Expression<Func<PlayGoldenGoalGame, bool>> filter)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<PlayGoldenGoalGame>().Include(x => x.GoldenGoalGame).SingleOrDefault()
                    : context.Set<PlayGoldenGoalGame>().Include(x => x.GoldenGoalGame).SingleOrDefault(filter);
            }
        }

        public override PlayGoldenGoalGame GetFirst(Expression<Func<PlayGoldenGoalGame, bool>> filter)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<PlayGoldenGoalGame>().Include(x => x.GoldenGoalGame).FirstOrDefault()
                    : context.Set<PlayGoldenGoalGame>().Include(x => x.GoldenGoalGame).FirstOrDefault(filter);
            }
        }
    }
}
