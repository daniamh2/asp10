﻿using System.Collections.Generic;

namespace project.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool status { get; set; }
        public ICollection<Product> Products { get;  }

    }
}
