using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Core.DataAccess.EntityFramework;
using Warehouse.Data.Interface;
using Warehouse.Entities;
using System.Data.Entity;
namespace Warehouse.Data.Data
{
    public class ForumArticleLikeDal : EntityRepositoryBase<ForumArticleLike, WarehouseContext>, IForumArticleLikeDal
    {
        public override ForumArticleLike GetFirst(Expression<Func<ForumArticleLike, bool>> filter)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<ForumArticleLike>().Include(x => x.ForumArticle).FirstOrDefault()
                    : context.Set<ForumArticleLike>().Include(x => x.ForumArticle).FirstOrDefault(filter);
            }
        }
    }
}