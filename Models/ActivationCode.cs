using System.Security.Claims;

namespace Team6.Models
{
    public class ActivationCode
    {
        public ActivationCode()
        {
            Code = Guid.NewGuid();
        }


        public Guid Code { get; set; }
        public int ProductID { get; set; }

        public int OrderID{ get; set; }

    }
}
