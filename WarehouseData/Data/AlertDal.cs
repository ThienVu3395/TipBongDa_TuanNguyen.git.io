using Warehouse.Core.DataAccess.EntityFramework;
using Warehouse.Data.Interface;
using Warehouse.Entities;
namespace Warehouse.Data.Data
{
    public class AlertDal : EntityRepositoryBase<Alert, WarehouseContext>, IAlertDal
    {
    }
}
