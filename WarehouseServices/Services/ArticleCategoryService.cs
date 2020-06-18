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
    public class ArticleCategoryService : IArticleCategoryService
    {
        readonly IArticleCategoryDal _articleCategoryDal;

        public ArticleCategoryService(IArticleCategoryDal articleCategoryDal)
        {
            _articleCategoryDal = articleCategoryDal;
        }

        public List<ArticleCategory> GetAll()
        {
            return _articleCategoryDal.GetList();
        }

        public ArticleCategory GetById(int Id)
        {
            return _articleCategoryDal.GetSingle(a => a.Id == Id);
        }

        public ArticleCategory GetByAlias(string alias)
        {
            return _articleCategoryDal.GetFirst(a => a.Alias == alias);
        }
    }
}
