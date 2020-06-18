using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Entities;

namespace Warehouse.Services.Interface
{
    public interface IForumArticleLikeService
    {
        bool CheckLike(int ArticleID, string UserName);
        void Add(ForumArticleLike forumArticleLike);
    }
}
