using System.Collections.Generic;
using Warehouse.Entities;

namespace Warehouse.Services.Interface
{
    public interface IBlogService
    {
        List<Blog> GetAll();
        List<Blog> GetListByDisplay(bool display);
        List<Blog> GetNewBlogs(int take);
        List<Blog> GetListHighLight();
        Blog GetById(int Id);
        Blog GetByAlias(string alias, bool? display = null);
        bool CheckUniqueTitle(string title);
        bool CheckUniqueAlias(string alias);
        void Add(Blog blog);
        void Update(Blog blog);
        void Delete(int Id);
        int CountDisplay();
        int CountHide();
        int CountByTitle(string Title);
    }
}
