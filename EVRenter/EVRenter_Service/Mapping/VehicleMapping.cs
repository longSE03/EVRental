using AutoMapper;
using EVRenter_Data.Entities;
using EVRenter_Service.RequestModel;
using EVRenter_Service.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVRenter_Service.Mapping
{
    public class VehicleMapping : Profile
    {
        public VehicleMapping()
        {
            CreateMap<Vehicle, VehicleResponseModel>()
                .ForMember(dest => dest.ModelName, opt => opt.MapFrom(src => src.Model.ModelName))
                .ForMember(dest => dest.StationName, opt => opt.MapFrom(src => src.Station.Name));
            CreateMap<VehicleRequestModel, Vehicle>();
            CreateMap<VehicleUpdateRequest, Vehicle>();
        }
    }
}
