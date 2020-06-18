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
    public class ForumArticleService : IForumArticleService
    {
        readonly IForumArticleDal _forumArticleDal;

        public ForumArticleService(IForumArticleDal forumArticleDal)
        {
            _forumArticleDal = forumArticleDal;
        }

        public void Add(ForumArticle forumArticle)
        {
            _forumArticleDal.Add(forumArticle);
        }

        public bool CheckUniqueAlias(string alias)
        {
            return _forumArticleDal.GetFirst(x => x.Alias == alias) != null;
        }

        public bool CheckUniqueTitle(string title)
        {
            return _forumArticleDal.GetFirst(x => x.Title == title) != null;
        }

        public void ConfirmArticle(int Id, string userConfirmed)
        {
            var entity = GetById(Id, null);
            entity.Display = true;
            entity.DateConfirmed = DateTime.Now;
            entity.UserConfirmed = userConfirmed;
            Update(entity);
        }

        public void Delete(int Id)
        {
            var entity = GetById(Id, null);
            _forumArticleDal.Delete(entity);
        }

        public List<ForumArticle> GetAll()
        {
            return _forumArticleDal.GetList().OrderByDescending(x => x.Id).ToList();
        }

        public ForumArticle GetByAlias(string alias, bool? display)
        {
            if(display == null)
                return _forumArticleDal.GetFirst(x => x.Alias == alias);
            else 
                return _forumArticleDal.GetFirst(x => x.Alias == alias && x.Display == display.Value);
        }

        public List<ForumArticle> GetByArticleCategoryId(int forumCategoryId, bool? display)
        {
            if (display == null)
                return _forumArticleDal.GetList(x => x.ForumCategoryId == forumCategoryId);
            else
                return _forumArticleDal.GetList(x => x.ForumCategoryId == forumCategoryId && x.Display == display.Value);
        }

        public ForumArticle GetById(int Id, bool? display)
        {
            if (display == null)
                return _forumArticleDal.GetSingle(x => x.Id == Id);
            else
                return _forumArticleDal.GetSingle(x => x.Id == Id && x.Display == display.Value);
        }

        public List<ForumArticle> GetListByDisplay(bool display)
        {
            return _forumArticleDal.GetList(x => x.Display == display);
        }

        public List<ForumArticle> GetListBySkipTake(int skip, int take)
        {
            return _forumArticleDal.GetList(x => x.Display == true).Skip(skip).Take(take).OrderByDescending(x => x.Id).ToList();
        }

        public void IncreaseView(int Id)
        {
            var entity = GetById(Id, null);
            entity.Views++;
            Update(entity);
        }

        public void Like(int Id)
        {
            var entity = GetById(Id, null);
            entity.Likes++;
            Update(entity);
        }

        public void UnLike(int Id)
        {
            var entity = GetById(Id, null);
            entity.Dislikes++;
            Update(entity);
        }

        public void UnConfirmArticle(int Id)
        {
            var entity = GetById(Id, null);
            entity.Display = false;
            entity.DateConfirmed = null;
            Update(entity);
        }

        public void Update(ForumArticle article)
        {
            _forumArticleDal.Update(article);
        }
    }
}
