using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PracAPI1.Models
{
    public class PagingOptions
    {
        [Range(1,999,ErrorMessage ="Must between 1-999")]
        public int? OffSet { get; set; }

        [Range(1,100,ErrorMessage ="Must between 1-100")]
        public int? Limit { get; set; }
    }
}
