using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracAPI1.Models
{
    public abstract class Resource : Link
    { 
        [JsonIgnore] //it will be on top of all serialized response
        public Link Self { get; set; }


    }
}
