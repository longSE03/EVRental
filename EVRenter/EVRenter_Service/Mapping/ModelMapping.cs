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
    public class ModelMapping : Profile
    {
        public ModelMapping()
        {
            CreateMap<Model, ModelResponseModel>()
                .ForMember(dest => dest.Vehicles, opt => opt.MapFrom(src => src.Vehicles.Where(v => !v.IsDelete)))
                .ForMember(dest => dest.RentalPrice, opt => opt.MapFrom(src => src.RentalPrice));
            CreateMap<ModelRequestModel, Model>();
            CreateMap<ModelUpdateRequest, Model>();
        }
    }
}
