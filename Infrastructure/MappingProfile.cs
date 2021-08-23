using AutoMapper;
using PracAPI1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracAPI1.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RoomEntity, Room>().ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate / 100m))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src => Link.To(
                    nameof(Controllers.RoomsController.GetRoomById),
                    new {id = src.Id})));
            //TODO: Url Link
            //define how entity objects are mapped to their corresponding resource objects
        }
    }
}
