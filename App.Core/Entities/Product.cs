using App.Core.Entities.Base;
using System.Collections.Generic;

namespace App.Core.Entities
{
    public class Product : CommonBaseNameBusinessEntity
    {
        public int Stock{ get; set; }
        public decimal Price { get; set; }

    }
}
