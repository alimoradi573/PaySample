using AutoMapper;
using Pay.OvetimePolicies.Application.DTOs;
using Pay.OvetimePolicies.Domain.Entities;

namespace Pay.OvetimePolicies.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<PayDTO, PayEntity>().ReverseMap();
        }
    }
}
