using Warehouse.Core.DataAccess.EntityFramework;
using Warehouse.Data.Interface;
using Warehouse.Entities;
namespace Warehouse.Data.Data
{
    public class HandbookDal : EntityRepositoryBase<Handbook, WarehouseContext>, IHandbookDal
    {
       
    }
}
