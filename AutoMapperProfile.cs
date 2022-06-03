using AutoMapper;
using backend_dotnet.Models;
using backend_dotnet.Dtos;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Employee, EmployeeDto>().ReverseMap(); //reverse so the both direction
    }
}