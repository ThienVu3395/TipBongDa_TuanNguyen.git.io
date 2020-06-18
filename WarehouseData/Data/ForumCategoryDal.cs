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
    public class ForumCategoryDal : EntityRepositoryBase<ForumCategory, WarehouseContext>, IForumCategoryDal
    {
        public override List<ForumCategory> GetList(Expression<Func<ForumCategory, bool>> filter = null)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<ForumCategory>().Include(x => x.ForumArticles).ToList()
                    : context.Set<ForumCategory>().Include(x => x.ForumArticles).Where(filter).ToList();
            }
        }

        public override ForumCategory GetFirst(Expression<Func<ForumCategory, bool>> filter)
        {
            using (var context = new WarehouseContext()) {
                return filter == null
                    ? context.Set<ForumCategory>().Include(x => x.ForumArticles).FirstOrDefault()
                    : context.Set<ForumCategory>().Include(x => x.ForumArticles).FirstOrDefault(filter);
            }
        }

        public override void Delete(ForumCategory forumCategory)
        {
            using (var context = new WarehouseContext())
            {
                context.ForumCategories.Attach(forumCategory);
                context.ForumCategories.Remove(forumCategory);
                context.SaveChanges();
            }
        }
    }
}
