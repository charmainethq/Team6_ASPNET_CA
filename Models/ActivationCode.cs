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
        public string ProductActivationCode { get; set; }
        public ActivationCode(int pID,int oID)
        {
            ProductID= pID;
            OrderID= oID;
            ProductActivationCode=Guid.NewGuid().ToString();
        }

    }
}
