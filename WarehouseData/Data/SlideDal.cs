using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Warehouse.Core.DataAccess.EntityFramework;
using Warehouse.Data.Interface;
using Warehouse.Entities;
using System.Linq.Expressions;

namespace Warehouse.Data.Data
{
    public class SlideDal : EntityRepositoryBase<Slide, WarehouseContext>, ISlideDal
    {
        public override int Count(Expression<Func<Slide, bool>> filter)
        {
            using (var context = new WarehouseContext()) {
                return filter == null
                    ? context.Set<Slide>().Count()
                    : context.Set<Slide>().Count(filter);
            }
        }
        public override List<Slide> GetList(Expression<Func<Slide, bool>> filter = null)
        {
            using (var context = new WarehouseContext()) {
                return filter == null
                    ? context.Set<Slide>().ToList()
                    : context.Set<Slide>().Where(filter).ToList();
            }
        }
        public override Slide GetFirst(Expression<Func<Slide, bool>> filter)
        {
            using (var context = new WarehouseContext()) {
                return filter == null
                    ? context.Set<Slide>().FirstOrDefault()
                    : context.Set<Slide>().FirstOrDefault(filter);
            }
        }
        public override Slide GetSingle(Expression<Func<Slide, bool>> filter)
        {
            using (var context = new WarehouseContext()) {
                return filter == null
                    ? context.Set<Slide>().SingleOrDefault()
                    : context.Set<Slide>().SingleOrDefault(filter);
            }
        }
        public override void Delete(Slide slide)
        {
            using (var context = new WarehouseContext())
            {
                context.Slides.Attach(slide);
                context.Slides.Remove(slide);
                context.SaveChanges();
            }
        }

    }
}
