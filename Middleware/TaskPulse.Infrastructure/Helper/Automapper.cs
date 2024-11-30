using AutoMapper;
using TaskPulse.Domain.Entities;
using TaskPulse.Domain.Entities.DTO;

namespace TaskPulse.Infrastructure.Helper;

public class Automapper : Profile
{
    public Automapper()
    {
        CreateMap<AddTask,TaskModel>()
            .ForMember(dest => dest.TaskId, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.PriorityId, opt => opt.MapFrom(src => src.PriorityId));


        CreateMap<UserRegistration, UsersData>()
            .ForMember(dest => dest.userId, opt => opt.Ignore())
            .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.username))
            .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.email))
            .ForMember(dest => dest.passwordHash, opt => opt.MapFrom(src => src.password))
            .ForMember(dest => dest.premium, opt => opt.MapFrom(src => src.premium))
            .ForMember(dest => dest.active, opt => opt.MapFrom(src => src.active))
            .ForMember(dest => dest.createdDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.updateDate, opt => opt.MapFrom(src => DateTime.Now));

    }
}