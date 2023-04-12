using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Team6.Models
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

        //Many Activation Code to one OrderItem
        public virtual OrderItem OrderItem { get; set; }

    }
}
