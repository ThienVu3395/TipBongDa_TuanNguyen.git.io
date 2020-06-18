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
    public class ForumCategoryService : IForumCategoryService
    {
        readonly IForumCategoryDal _forumCategoryDal;

        public ForumCategoryService(IForumCategoryDal forumCategoryDal)
        {
            _forumCategoryDal = forumCategoryDal;
        }

        public void Add(ForumCategory forumCategory)
        {
            _forumCategoryDal.Add(forumCategory);
        }

        public bool CheckUniqueAlias(string alias)
        {
            return _forumCategoryDal.GetFirst(x => x.Alias == alias) == null;
        }

        public bool CheckUniqueName(string name)
        {
            return _forumCategoryDal.GetFirst(x => x.Name == name) == null;
        }

        public void Delete(int Id)
        {
            var forumCategory = GetById(Id, null);
            if(forumCategory != null)
            {
                _forumCategoryDal.Delete(forumCategory);
            }
        }

        public List<ForumCategory> GetAll()
        {
            return _forumCategoryDal.GetList().OrderBy(x => x.SortOrder).ToList();
        }

        public ForumCategory GetByAlias(string alias, bool? display)
        {
            return _forumCategoryDal.GetFirst(x => x.Alias == alias);
        }

        public ForumCategory GetById(int Id, bool? display)
        {
            if(display == null)
                return _forumCategoryDal.GetSingle(x => x.Id == Id);
            else
                return _forumCategoryDal.GetSingle(x => x.Id == Id && x.Display == display);
        }

        public List<ForumCategory> GetListByDisplay(bool display)
        {
            return _forumCategoryDal.GetList(x => x.Display == display).OrderBy(x => x.SortOrder).ToList();
        }

        public void Update(ForumCategory forumCategory)
        {
            _forumCategoryDal.Update(forumCategory);
        }
    }
}
