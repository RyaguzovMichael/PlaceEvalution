using AutoMapper;
using PlaceEvolution.API.API.Models.ViewModels;
using PlaceEvolution.API.Domain;
using PlaceEvolution.API.Domain.Services;
using PlaceEvolution.API.Infrastructure.Models.DbModels;

namespace PlaceEvolution.API.API.DependencyInjection;

public static class MapperConfigurator
{
    public static IServiceCollection AddAutomapper(this IServiceCollection services)
    {
        UserFactory userFactory = services.BuildServiceProvider().GetService<UserFactory>()!;
        services.AddSingleton(provider =>
            new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutomapperProfile(userFactory));
                cfg.AddProfile(new DbModelsMappingProfile());
            }).CreateMapper());
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
                .ForMember(r => r.Customer, o => o.MapFrom(p => userFactory.GetCustomer(p.User)));
            CreateMap<Place, PlaceShortVm>()
                .ForMember(r => r.PhotosCount, o => o.MapFrom(p => p.Photos == null ? 0 : p.Photos.Count))
                .ForMember(r => r.ReviewsCount, o => o.MapFrom(p => p.Reviews == null ? 0 : p.Reviews.Count));
        }
    }

    private class DbModelsMappingProfile : Profile
    {
        public DbModelsMappingProfile()
        {
            CreateMap<Review, ReviewDbModel>()
                .ForMember(r => r.CreatedDate, o => o.MapFrom(_ => DateTime.Now))
                .ForMember(r => r.LastModifiedDate, o => o.MapFrom(_ => DateTime.Now));
        }
    }
}