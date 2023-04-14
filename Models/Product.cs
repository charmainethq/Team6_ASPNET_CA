﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team6.Models
{
    [Table("Products")]
    public class Product
    {

        public string ProductId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public float Price { get; set; }

    }
}
