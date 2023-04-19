using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team6.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public int Rating { get; set; } 
        public string ReviewText { get; }   

    }
}
