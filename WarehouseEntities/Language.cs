using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Core.Entities;

namespace Warehouse.Entities
{
    [Table("Language")]
    public partial class Language: IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Use { get; set; }
        public Nullable<byte> SortOrder { get; set; }

    }
}
