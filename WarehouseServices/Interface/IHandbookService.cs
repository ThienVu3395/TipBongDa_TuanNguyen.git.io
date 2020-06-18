using System.Collections.Generic;
using Warehouse.Entities;

namespace Warehouse.Services.Interface
{
    public interface IHandbookService
    {
        List<Handbook> GetAll();
        List<Handbook> GetListByDisplay(bool display);
        List<Handbook> GetListHighLight();
        List<Handbook> GetNewHandbooks(int take);

        Handbook GetById(int Id);
        Handbook GetByAlias(string alias, bool? display = null);
        bool CheckUniqueTitle(string title);
        bool CheckUniqueAlias(string alias);
        void Add(Handbook handbook);
        void Update(Handbook handbook);
        void Delete(int Id);
        int CountDisplay();
        int CountHide();
        int CountByTitle(string Title);
    }
}
