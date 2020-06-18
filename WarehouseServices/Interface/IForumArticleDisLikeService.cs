using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Entities;

namespace Warehouse.Services.Interface
{
    public interface IForumArticleDisLikeService
    {
        bool CheckDisLike(int ArticleID, string UserName);
        void Add(ForumArticleDisLike forumArticleDisLike);
    }
}
