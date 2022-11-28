using AutoMapper;
using TestApplication.BL.DTO;
using TestApplication.DAL.Entities;

namespace TestApplication.BL.Mapping;

public class BLMapper : Profile
{
    public BLMapper()
    {
        CreateMap<TableADTO, TableA>().ReverseMap();
        CreateMap<TableBDTO, TableB>().ReverseMap();
        CreateMap<TableCDTO, TableC>().ReverseMap();
    }
}
