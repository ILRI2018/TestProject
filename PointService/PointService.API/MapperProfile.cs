using AutoMapper;
using PointService.DataAccess.Models;
using PointService.ViewModels;

namespace PointService.API
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Client, ClientVM>();
            CreateMap<Client, ClientVM>().ReverseMap();
            CreateMap<Transaction, TransactionVM>();
            CreateMap<Transaction, TransactionVM>().ReverseMap();
        }
    }
}
