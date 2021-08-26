using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }

        public Customer Customer { get; set; }
        public Movie Movie { get; set; }
    }
}
