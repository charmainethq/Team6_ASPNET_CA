using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace TicTacToe.Models
{
    public class ActivationCode
    {
        public ActivationCode()
        {
            Code = Guid.NewGuid();
        }

        [Key]
        public Guid Code { get; set; }
        public string ProductID { get; set; }

        public string OrderID{ get; set; }


    }
}
