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
    public class ArticleCategoryDal : EntityRepositoryBase<ArticleCategory, WarehouseContext>, IArticleCategoryDal
    {
        public override List<ArticleCategory> GetList(Expression<Func<ArticleCategory, bool>> filter = null)
        {
            using (var context = new WarehouseContext()) {
                return filter == null
                    ? context.Set<ArticleCategory>().Include(x => x.Articles).ToList()
                    : context.Set<ArticleCategory>().Include(x => x.Articles).Where(filter).ToList();
            }
        }
        public override ArticleCategory GetSingle(Expression<Func<ArticleCategory, bool>> filter)
        {
            using (var context = new WarehouseContext()) {
                return filter == null
                    ? context.Set<ArticleCategory>().Include(x => x.Articles).SingleOrDefault()
                    : context.Set<ArticleCategory>().Include(x => x.Articles).SingleOrDefault(filter);
            }
        }

        public override ArticleCategory GetFirst(Expression<Func<ArticleCategory, bool>> filter)
        {
            using (var context = new WarehouseContext()) {
                return filter == null
                    ? context.Set<ArticleCategory>().Include(x => x.Articles).FirstOrDefault()
                    : context.Set<ArticleCategory>().Include(x => x.Articles).FirstOrDefault(filter);
            }
        }
        public override int Count(Expression<Func<ArticleCategory, bool>> filter)
        {
            using (var context = new WarehouseContext()) {
                return filter == null
                    ? context.Set<ArticleCategory>().Count()
                    : context.Set<ArticleCategory>().Count(filter);
            }
        } 

        public override void Delete(ArticleCategory articleCategory)
        {
            using (var context = new WarehouseContext()) {
                context.ArticleCategories.Attach(articleCategory);
                context.ArticleCategories.Remove(articleCategory);
                context.SaveChanges();
            }
        }


    }
}
