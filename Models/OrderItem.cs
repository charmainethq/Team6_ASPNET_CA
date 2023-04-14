using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace Team6.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public string OrderID { get; set; }
        public string ProductID { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public List<ActivationCode> ActivationCodes { get; set; }

        public OrderItem() 
        {
            ActivationCodes = new List<ActivationCode>();   
        }


    }
}
