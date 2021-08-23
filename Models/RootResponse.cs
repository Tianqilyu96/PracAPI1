using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracAPI1.Models
{
    public class RootResponse : Resource
    {
        public Link Info { get; set; }

        public Link Rooms { get; set; }
    }
}
