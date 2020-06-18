using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Data.Interface;
using Warehouse.Entities;
using Warehouse.Services.Interface;

namespace Warehouse.Services.Services
{
    public class ArticleService : IArticleService
    {
        readonly IArticleDal _articleDal;

        public ArticleService(IArticleDal articleDal)
        {
            _articleDal = articleDal;
        }

        public List<Article> GetAll()
        {
            return _articleDal.GetList();
        }

        public List<Article> GetByHot(int articleCategoryId)
        {
            return _articleDal.GetList(x => x.Hot && x.ArticleCategoryId == articleCategoryId);
        }

        public List<Article> GetByArticleCategoryId(int articleCategoryId, bool? display)
        {
            if(display == null)
                return _articleDal.GetList(x => x.ArticleCategoryId == articleCategoryId);
            else
                return _articleDal.GetList(x => x.ArticleCategoryId == articleCategoryId && x.Display == display);
        }

        public List<Article> GetListByDisplay(bool? Display = null)
        {
            return _articleDal.GetList(a => a.Display == Display);
        }

        public Article GetByAlias(string alias, bool? display)
        {
            if(display == null)
                return _articleDal.GetSingle(x => x.Alias == alias);
            else
                return _articleDal.GetSingle(x => x.Alias == alias && x.Display == display);
        }

        public Article GetById(int Id)
        {
            return _articleDal.GetSingle(a => a.Id == Id);
        }

        public bool CheckUniqueTitle(string Title)
        {
            return _articleDal.GetFirst(b => b.Title == Title) == null;
        }

        public bool CheckUniqueAlias(string Alias)
        {
            return _articleDal.GetFirst(b => b.Alias == Alias) == null;
        }

        public void Add(Article article)
        {
            _articleDal.Add(article);
        }

        public void Update(Article article)
        {
            _articleDal.Update(article);
        }

        public void Delete(int Id)
        {
            Article article = GetById(Id);
            if(article != null)
                _articleDal.Delete(article);
        }
    }
}
