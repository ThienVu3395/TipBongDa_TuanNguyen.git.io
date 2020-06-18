using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Warehouse.Core.DataAccess.EntityFramework;
using Warehouse.Data.Interface;
using Warehouse.Entities;

namespace Warehouse.Data.Data
{
    public class LeagueDal : EntityRepositoryBase<League, WarehouseContext>, ILeagueDal
    {

    }
}
