using System.Collections.Generic;
using Warehouse.Entities;

namespace Warehouse.Services.Interface
{
    public interface IProphet1x2GameDetailService
    {
        List<Prophet1x2GameDetail> GetAll();
        List<Prophet1x2GameDetail> GetByProphet1x2GameId(int Prophet1x2GameId);
        List<Prophet1x2GameDetail> GetByRound(int Round);
        Prophet1x2GameDetail GetById(int Id);
        bool CheckUserHavePlayed(int Id, string userName);
        void Add(Prophet1x2GameDetail prophet1x2GameDetail);
        void Update(Prophet1x2GameDetail prophet1x2GameDetail);
        void Delete(Prophet1x2GameDetail prophet1x2GameDetail);
    }
}
