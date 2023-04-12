using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team6.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public string OrderID { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }

        //an orderItem is linked to a single Product 
        public virtual Product Products { get; set; }

        // one Order Item can have multiple ActivationCode depending on the quantity purchased
        public virtual ICollection<ActivationCode> ActivationCodes { get; set; }

    }
}
