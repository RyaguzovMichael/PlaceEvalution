using AutoMapper;
using LastExamBackEndProject.API.Models.ViewModels;
using LastExamBackEndProject.Domain;

namespace LastExamBackEndProject.API.DependencyInjection;

public static class MapperConfigurator
{
    public static IServiceCollection AddAutomapper(this IServiceCollection services)
    {
        services.AddSingleton(provider =>
            new MapperConfiguration(cfg => { cfg.AddProfile(new AutomapperProfile()); }).CreateMapper());
        return services;
    }

    private class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Customer, CustomerVm>();
        }
    }
}