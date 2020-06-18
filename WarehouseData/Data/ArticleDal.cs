using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Warehouse.Core.DataAccess.EntityFramework;
using Warehouse.Data.Interface;
using Warehouse.Entities;
using System.Data.Entity;
namespace Warehouse.Data.Data
{
    public class ArticleDal : EntityRepositoryBase<Article, WarehouseContext>, IArticleDal
    {
        public override List<Article> GetList(Expression<Func<Article, bool>> filter = null)
        {
            using (var context = new WarehouseContext()) {
                return filter == null
                    ? context.Set<Article>().Include(x => x.ArticleCategory).ToList()
                    : context.Set<Article>().Include(x => x.ArticleCategory).Where(filter).ToList();
            }
        }

        public override Article GetSingle(Expression<Func<Article, bool>> filter)
        {
            using (var context = new WarehouseContext()) {
                return filter == null
                    ? context.Set<Article>().Include(x => x.ArticleCategory).SingleOrDefault()
                    : context.Set<Article>().Include(x => x.ArticleCategory).SingleOrDefault(filter);
            }
        }

        public override Article GetFirst(Expression<Func<Article, bool>> filter)
        {
            using (var context = new WarehouseContext()) {
                return filter == null
                    ? context.Set<Article>().Include(x => x.ArticleCategory).FirstOrDefault()
                    : context.Set<Article>().Include(x => x.ArticleCategory).FirstOrDefault(filter);
            }
        }

        public override int Count(Expression<Func<Article, bool>> filter)
        {
            using (var context = new WarehouseContext()) {
                return filter == null
                    ? context.Set<Article>().Count()
                    : context.Set<Article>().Count(filter);
            }
        } 

        public override void Delete(Article article)
        {
            using (var context = new WarehouseContext()) {
                context.Articles.Attach(article);
                context.Articles.Remove(article);
                context.SaveChanges();
            }
        }


    }
}
