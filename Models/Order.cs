using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team6.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        // might need dictionary of orderitem:orderquantity

        public DateTime OrderDate { get; set; }
   

    }
}
