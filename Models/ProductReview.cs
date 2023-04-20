using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team6.Models
{
    public class ProductReview
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }
        public int OrderId { get; set; }

        public int? Rating { get; set; } 
        public string? ReviewText { get; set; }   

    }
}

