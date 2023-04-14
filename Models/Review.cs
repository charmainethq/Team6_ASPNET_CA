﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team6.Models
{

    // this is a test comment under the review model, from JL
    public class Review
    {
        public int Id { get; set; }
        public int ProductID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }
        public int OrderId { get; set; }
        public int Rating { get; set; } 

        public virtual Order Order { get; set; }
    }
}
