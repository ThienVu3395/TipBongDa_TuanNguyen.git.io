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
    public class GoldenGoalGameDal : EntityRepositoryBase<GoldenGoalGame, WarehouseContext>, IGoldenGoalGameDal
    {
        public override List<GoldenGoalGame> GetList(Expression<Func<GoldenGoalGame, bool>> filter = null)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<GoldenGoalGame>().Include(x => x.PlayGoldenGoalGames).ToList()
                    : context.Set<GoldenGoalGame>().Include(x => x.PlayGoldenGoalGames).Where(filter).ToList();
            }
        }
        public override GoldenGoalGame GetSingle(Expression<Func<GoldenGoalGame, bool>> filter)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<GoldenGoalGame>().Include(x => x.PlayGoldenGoalGames).SingleOrDefault()
                    : context.Set<GoldenGoalGame>().Include(x => x.PlayGoldenGoalGames).SingleOrDefault(filter);
            }
        }

        public override GoldenGoalGame GetFirst(Expression<Func<GoldenGoalGame, bool>> filter)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<GoldenGoalGame>().Include(x => x.PlayGoldenGoalGames).FirstOrDefault()
                    : context.Set<GoldenGoalGame>().Include(x => x.PlayGoldenGoalGames).FirstOrDefault(filter);
            }
        }
    }
}
