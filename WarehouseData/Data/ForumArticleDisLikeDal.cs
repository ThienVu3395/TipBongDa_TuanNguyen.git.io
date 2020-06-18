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
    public class ForumArticleDisLikeDal : EntityRepositoryBase<ForumArticleDisLike, WarehouseContext>, IForumArticleDisLikeDal
    {
        public override ForumArticleDisLike GetFirst(Expression<Func<ForumArticleDisLike, bool>> filter)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<ForumArticleDisLike>().Include(x => x.ForumArticle).FirstOrDefault()
                    : context.Set<ForumArticleDisLike>().Include(x => x.ForumArticle).FirstOrDefault(filter);
            }
        }
    }
}
