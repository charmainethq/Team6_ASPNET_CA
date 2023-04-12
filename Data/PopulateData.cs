using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Team6.Models;

namespace Team6.Data
{
    public class SampleData
    {
        private readonly ShopContext db;
        public SampleData(ShopContext db)
        {
            this.db = db;
        }

        public void AddSampleData()
        {
            Customer cust1 = new Customer
            {
                CustomerID = "10095",
                Username = "cherwah",
                Password = "pass",
                FirstName = "Cher Wah",
                LastName = "Tan"
            };

            db.Add(cust1);
            db.SaveChanges();

        }

    }
}
