using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicTacToe.Models
{
    [Table("Products")]
    public class Product
    {

        [Key]
        public string ProductId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public float Price { get; set; }

    }
}
