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
    public class ForumArticleDal : EntityRepositoryBase<ForumArticle, WarehouseContext>, IForumArticleDal
    {
        public override List<ForumArticle> GetList(Expression<Func<ForumArticle, bool>> filter = null)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<ForumArticle>().Include(x => x.ForumCategory).Include(x => x.ForumComments).ToList()
                    : context.Set<ForumArticle>().Include(x => x.ForumCategory).Include(x => x.ForumComments).Where(filter).ToList();
            }
        }

        public override ForumArticle GetFirst(Expression<Func<ForumArticle, bool>> filter)
        {
            using (var context = new WarehouseContext()) {
                return filter == null
                    ? context.Set<ForumArticle>().Include(x => x.ForumCategory).Include(x => x.ForumComments).FirstOrDefault()
                    : context.Set<ForumArticle>().Include(x => x.ForumCategory).Include(x => x.ForumComments).FirstOrDefault(filter);
            }
        }

        //public override void Delete(ForumArticle forumArticles)
        //{
        //    using (var context = new WarehouseContext()) {
        //        context.ForumArticles.Attach(forumArticles);
        //        context.ForumArticles.Remove(forumArticles);
        //        context.SaveChanges();
        //    }
        //}
    }
}
