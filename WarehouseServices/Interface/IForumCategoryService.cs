using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Entities;

namespace Warehouse.Services.Interface
{
    public interface IForumCategoryService
    {
        List<ForumCategory> GetAll();
        List<ForumCategory> GetListByDisplay(bool display);
        ForumCategory GetById(int Id, bool? display);
        ForumCategory GetByAlias(string alias, bool? display);
        bool CheckUniqueName(string name);
        bool CheckUniqueAlias(string alias);
        void Add(ForumCategory forumCategory);
        void Update(ForumCategory forumCategory);
        void Delete(int Id);
    }
}
