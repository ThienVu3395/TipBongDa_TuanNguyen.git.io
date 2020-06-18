using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Warehouse.Core.DataAccess.EntityFramework;
using Warehouse.Data.Interface;
using Warehouse.Entities;

namespace Warehouse.Data.Data
{
    public class InfoWebDal : EntityRepositoryBase<InfoWeb, WarehouseContext>, IInfoWebDal
    {
       

    }
}
