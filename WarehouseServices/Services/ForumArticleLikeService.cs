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
    public class ForumArticleLikeService : IForumArticleLikeService
    {
        readonly IForumArticleLikeDal _forumArticleLikeDal;

        public ForumArticleLikeService(IForumArticleLikeDal forumArticleLikeDal)
        {
            _forumArticleLikeDal = forumArticleLikeDal;
        }

        public void Add(ForumArticleLike forumArticleLike)
        {
            _forumArticleLikeDal.Add(forumArticleLike);
        }

        public bool CheckLike(int ArticleID, string UserName)
        {
            var check = _forumArticleLikeDal.GetFirst(x => x.ArticleId == ArticleID && x.UserName == UserName);

            if(check == null)
            {
                return false;
            }
            return true;
        }
    }
}
