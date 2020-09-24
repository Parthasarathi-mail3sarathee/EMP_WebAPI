﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class Login
    {
        [StringLength(20), Required]
        public string username { get; set; }
        [StringLength(20), Required]
        public string password { get; set; }
    }
}
