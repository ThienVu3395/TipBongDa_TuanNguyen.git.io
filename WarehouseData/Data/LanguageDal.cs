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
    public class LanguageDal : EntityRepositoryBase<Language, WarehouseContext>, ILanguageDal
    {
        public override List<Language> GetList(Expression<Func<Language, bool>> filter = null)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<Language>().ToList()
                    : context.Set<Language>().Where(filter).ToList();
            }
        }
    }
}
