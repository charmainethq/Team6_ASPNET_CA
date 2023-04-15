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
        public string ProductID { get; set; }

        public string OrderID{ get; set; }

        public ActivationCode(string pID, string oID)
        {
            ProductID = pID;
            OrderID = oID;
            ProductActivationCode = Guid.NewGuid().ToString();
        }

    }
}
