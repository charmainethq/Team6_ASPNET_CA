using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team6.Models
{
    public class Order
    {
        public string OrderID { get; set; }
        public string CustomerID { get; set; }
        // might need dictionary of orderitem:orderquantity

        public DateTime OrderDate { get; set; }




    }
}
