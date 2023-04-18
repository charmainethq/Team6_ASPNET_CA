using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace Team6.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string ProductDescription { get; set; }
        public string ProductName { get; set; }
        public List<ActivationCode> ActivationCodes { get; set; }
        public string ProductImage { get; set; }
        public OrderItem() 
        {
            ActivationCodes = new List<ActivationCode>();
            ActivationCodes.Add(new ActivationCode());


		}


    }
}
