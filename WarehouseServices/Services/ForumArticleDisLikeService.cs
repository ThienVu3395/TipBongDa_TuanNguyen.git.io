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
    public class ForumArticleDisLikeService : IForumArticleDisLikeService
    {
        readonly IForumArticleDisLikeDal _forumArticleDisLikeDal;

        public ForumArticleDisLikeService(IForumArticleDisLikeDal forumArticleDisLikeDal)
        {
            _forumArticleDisLikeDal = forumArticleDisLikeDal;
        }

        public void Add(ForumArticleDisLike forumArticleDisLike)
        {
            _forumArticleDisLikeDal.Add(forumArticleDisLike);
        }

        public bool CheckDisLike(int ArticleID, string UserName)
        {
            var check = _forumArticleDisLikeDal.GetSingle(x => x.ArticleId == ArticleID && x.UserName == UserName);
            if (check == null)
            {
                return false;
            }
            return true;
        }
    }
}
