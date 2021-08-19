using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PracAPI1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracAPI1.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly HotelInfo _info;

        public InfoController(IOptions<HotelInfo> hotelInfoWrapper) //Ioptions is a wrapper around of any data that you push into the service container
        {
            _info = hotelInfoWrapper.Value;
        }
        [HttpGet(Name = nameof(GetInfo))]
        [ProducesResponseType(200)]
        public ActionResult<HotelInfo> GetInfo() //return a strongly typed model  from this method
        {
            _info.Href = Url.Link(nameof(GetInfo), null);
            return _info;
        }
    }
}
