namespace StayFit.Services.Data.Tests
{
    using System.Reflection;

    using AutoMapper;
    using StayFit.Services.Mapping;

    public class MapperInitializationProfile : Profile
    {
        public MapperInitializationProfile()
        {
            AutoMapperConfig.RegisterMappings(Assembly.GetCallingAssembly());
        }
    }
}
