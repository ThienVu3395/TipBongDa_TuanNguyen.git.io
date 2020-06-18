using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Entities;

namespace Warehouse.Services.Interface
{
    public interface IForumCommentService
    {
        List<ForumComment> GetByArticleId(int articleId, bool? display);
        List<ForumComment> GetByParentCommentId(int commentID, bool? display);
        void Add(ForumComment forumComment);
        void Update(ForumComment forumComment);
        void Delete(int Id);
        void Like(int Id);
        void UnLike(int Id);
    }
}
