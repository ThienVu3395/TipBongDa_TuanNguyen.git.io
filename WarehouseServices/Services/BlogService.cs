using System.Collections.Generic;
using System.Linq;
using Warehouse.Data.Interface;
using Warehouse.Entities;
using Warehouse.Services.Interface;

namespace Warehouse.Services.Services
{
    public class BlogService : IBlogService
    {
        IBlogDal _blogDal;

        public BlogService(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }

        public List<Blog> GetAll()
        {
            return _blogDal.GetList();
        }

        public List<Blog> GetListByDisplay(bool Display)
        {
           return _blogDal.GetList(b => b.Display == Display);
        }

        public Blog GetByAlias(string alias, bool? display = null)
        {
            if (display == null)
                return _blogDal.GetSingle(b => b.Alias == alias);
            else
                return _blogDal.GetSingle(x => x.Alias == alias && x.Display == display.Value);
        }

        public Blog GetById(int Id)
        {
             return _blogDal.GetSingle(b => b.Id == Id);
        }

        public bool CheckUniqueTitle(string Title)
        {
            return _blogDal.GetFirst(b => b.Title == Title) == null;
        }

        public bool CheckUniqueAlias(string Alias)
        {
            return _blogDal.GetFirst(b => b.Alias == Alias) == null;
        }

        public void Add(Blog blog)
        {
            _blogDal.Add(blog);
        }

        public void Update(Blog blog)
        {
            _blogDal.Update(blog);
        }

        public void Delete(int Id)
        {
            Blog blog = GetById(Id);
            if (blog != null)
                _blogDal.Delete(blog);
        }
        
        public int CountDisplay()
        {
            return _blogDal.Count(b => b.Display == true);
        }

        public int CountHide()
        {
            return _blogDal.Count(b => b.Display == false);
        }

        public int CountByTitle(string Title)
        {
            return _blogDal.Count(x => x.Title == Title);
        }

        public List<Blog> GetNewBlogs(int take)
        {
            return _blogDal.GetList(b => b.Display == true).OrderByDescending(x => x.Id).Take(take).ToList();
        }

        public List<Blog> GetListHighLight()
        {
            return _blogDal.GetList(b => b.Display == true && b.IsHighlight == true).OrderByDescending(x => x.Id).ToList();
        }
    }
}
