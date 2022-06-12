using System;
using System.Collections.Generic;

#nullable disable

namespace Today.Model.Models
{
    public partial class CarModel
    {
        public CarModel()
        {
            OrderDetais = new HashSet<OrderDetai>();
        }

        public int CarModelId { get; set; }
        public string CarModel1 { get; set; }

        public virtual ICollection<OrderDetai> OrderDetais { get; set; }
    }
}
