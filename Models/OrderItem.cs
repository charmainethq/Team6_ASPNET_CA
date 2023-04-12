using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public string OrderID { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }

        // one Order Item can have multiple ActivationCode depending on the quantity purchased
        public virtual List<ActivationCode> ActivationCodes { get; set; }

        public OrderItem()
        {
            ActivationCodes = new List<ActivationCode>();
        }
    }
}
