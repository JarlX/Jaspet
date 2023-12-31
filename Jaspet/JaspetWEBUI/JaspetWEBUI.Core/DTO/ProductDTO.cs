﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JaspetWEBUI.Core.DTO
{
    public class ProductDTO
    {
        public Guid Guid { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Image { get; set; }
        
        public string CategoryName { get; set; }
        
        public Guid? CategoryGuid { get; set; }
        
        public double? UnitPrice { get; set; }

    }
}
