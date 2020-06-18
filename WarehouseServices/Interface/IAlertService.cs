using System.Collections.Generic;
using Warehouse.Entities;

namespace Warehouse.Services.Interface
{
    public interface IAlertService
    {
        List<Alert> GetAll();
        List<Alert> GetToShow();
        int CountListToShow();
        Alert GetById(int Id);
        void Add(Alert article);
        void Update(Alert article);
        void Delete(int Id);
    }
}
