using AutoMapper;
using LastExamBackEndProject.API.Models.ViewModels;
using LastExamBackEndProject.Domain;
using LastExamBackEndProject.Domain.Services;

namespace LastExamBackEndProject.API.DependencyInjection;

public static class MapperConfigurator
{
    public static IServiceCollection AddAutomapper(this IServiceCollection services)
    {
        UserFactory userFactory = services.BuildServiceProvider().GetService<UserFactory>()!;
        services.AddSingleton(provider =>
            new MapperConfiguration(cfg => { cfg.AddProfile(new AutomapperProfile(userFactory)); }).CreateMapper());
        return services;
    }

    private class AutomapperProfile : Profile
    {
        public AutomapperProfile(UserFactory userFactory)
        {
            CreateMap<Customer, CustomerVm>();
            CreateMap<Place, PlaceVm>();
            CreateMap<Review, ReviewVm>()
                .ForMember(r => r.ReviewDate, o => o.MapFrom(p => p.ReviewDate.ToLongDateString()))
                .ForMember(r => r.Customer, o => o.MapFrom( p => userFactory.GetCustomer(p.User)));
            CreateMap<Place, PlaceShortVm>()
                .ForMember(r => r.PhotosCount, o => o.MapFrom(p => p.Photos.Count ?? 0))
                .ForMember(r => r.ReviewsCount, o => o.MapFrom(p => p.Reviews.Count ?? 0));
        }
    }
}