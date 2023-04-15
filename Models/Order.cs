using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team6.Models
{
    public class Order
    {
        [Key]
        public string OrderID { get; set; }
        public string CustomerID { get; set; }
        public string OrderStatus { get; set; }
        // one to many Order to OrderItem
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual Review Review { get; set; }
        public DateTime OrderDate { get; set; }
   

    }
}
