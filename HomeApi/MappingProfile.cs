using AutoMapper;
using HomeApi.Configuration;
using HomeApi.Contracts;
using HomeApi.Contracts.Devices;
using HomeApi.Contracts.Models.Devices;
using HomeApi.Contracts.Models.Rooms;
using HomeApi.Data.Models;

namespace HomeApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddressInfo>();
            CreateMap<HomeOptions, InfoResponse>()
                .ForMember(m => m.AddressInfo,
                opt => opt.MapFrom(src => src.Address));

            // Валидация запросов:
            CreateMap<AddDeviceRequest, Device>()
                .ForMember(d => d.Location,
                    opt => opt.MapFrom(r => r.RoomLocation));
            CreateMap<AddRoomRequest, Room>();
            CreateMap<Device, DeviceView>();
        }
    }
}
