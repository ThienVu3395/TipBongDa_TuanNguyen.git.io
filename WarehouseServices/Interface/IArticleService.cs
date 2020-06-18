using System.Collections.Generic;
using Warehouse.Entities;

namespace Warehouse.Services.Interface
{
    public interface IArticleService
    {
        List<Article> GetAll();
        List<Article> GetByHot(int articleCategoryId);
        List<Article> GetByArticleCategoryId(int articleCategoryId, bool? display);
        List<Article> GetListByDisplay(bool? display);
        Article GetById(int Id);
        Article GetByAlias(string alias, bool? display);
        bool CheckUniqueTitle(string Title);
        bool CheckUniqueAlias(string Alias);
        void Add(Article article);
        void Update(Article article);
        void Delete(int Id);
    }
}
