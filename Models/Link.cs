using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PracAPI1.Models
{
    public class Link
    {
        public static Link To(string routeName, object routeValue = null)
            => new Link
            {
                RouteName = routeName,
                RouteValue = routeValue,
                Method = "GET",
                Relations = null
            };

        [JsonProperty(Order = -4)]
        public string Href { get; set; }

        [JsonProperty(Order = -3,PropertyName ="ref",NullValueHandling =NullValueHandling.Ignore)]
        public string Method { get; set; }

        [JsonProperty(Order = -2,DefaultValueHandling = DefaultValueHandling.Ignore,NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue("GET")]
        public string[] Relations { get; set; }

        //Store the route name and val before being rewriten by the rewritting fillter
        [JsonIgnore]
        public string RouteName { get; set; }
        [JsonIgnore]
        public object RouteValue { get; set; } //route value should be an object
    }
}
