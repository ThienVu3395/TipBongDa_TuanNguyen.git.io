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
    public class ForumCommentDal : EntityRepositoryBase<ForumComment, WarehouseContext>, IForumCommentDal
    {
        public override List<ForumComment> GetList(Expression<Func<ForumComment, bool>> filter = null)
        {
            using (var context = new WarehouseContext())
            {
                return filter == null
                    ? context.Set<ForumComment>().Include(x => x.ForumCommentParent).Include(x => x.ForumArticle).Include(x => x.ForumChildComments).ToList()
                    : context.Set<ForumComment>().Include(x => x.ForumCommentParent).Include(x => x.ForumArticle).Include(x => x.ForumChildComments).Where(filter).ToList();
            }
        }
    }
}
