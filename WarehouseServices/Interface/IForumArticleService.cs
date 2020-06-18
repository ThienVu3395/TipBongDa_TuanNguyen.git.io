using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Entities;

namespace Warehouse.Services.Interface
{
    public interface IForumArticleService
    {
        List<ForumArticle> GetAll();
        List<ForumArticle> GetByArticleCategoryId(int articleCategoryId, bool? display);
        List<ForumArticle> GetListByDisplay(bool display);
        List<ForumArticle> GetListBySkipTake(int skip, int take);
        ForumArticle GetById(int Id, bool? display);
        ForumArticle GetByAlias(string alias, bool? display);
        bool CheckUniqueTitle(string title);
        bool CheckUniqueAlias(string alias);
        void Add(ForumArticle forumArticle);
        void Update(ForumArticle forumArticle);
        void Delete(int Id);
        void Like(int Id);
        void UnLike(int Id);
        void IncreaseView(int Id);
        void ConfirmArticle(int Id, string userConfirmed);
        void UnConfirmArticle(int Id);
    }
}
