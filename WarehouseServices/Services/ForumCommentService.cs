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
    public class ForumCommentService : IForumCommentService
    {
        readonly IForumCommentDal _forumCommentDal;

        public ForumCommentService(IForumCommentDal forumCommentDal)
        {
            _forumCommentDal = forumCommentDal;
        }

        public void Add(ForumComment forumComment)
        {
            _forumCommentDal.Add(forumComment);
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<ForumComment> GetByArticleId(int articleId, bool? display)
        {
            if(display == null)
            {
                return _forumCommentDal.GetList(x => x.ForumArticleID == articleId).OrderByDescending(x => x.Id).ToList();
            }
            return _forumCommentDal.GetList(x => x.ForumArticleID == articleId && x.Display == display).OrderByDescending(x => x.Id).ToList();
        }

        public List<ForumComment> GetByParentCommentId(int commentParentID, bool? display)
        {
            if (display == null)
            {
                return _forumCommentDal.GetList(x => x.Id == commentParentID);
            }
            return _forumCommentDal.GetList(x => x.Id == commentParentID && x.Display == display);
        }

        public void Like(int Id)
        {
            throw new NotImplementedException();
        }

        public void UnLike(int Id)
        {
            throw new NotImplementedException();
        }

        public void Update(ForumComment article)
        {
            throw new NotImplementedException();
        }
    }
}
