﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracAPI1.Models
{
    public class HotelInfo : Resource
    {
        public string Title { get; set; }
        public string Tagline { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

    }
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; } 
    }
}
