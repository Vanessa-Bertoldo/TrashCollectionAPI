using AutoMapper;
using TrashCollectionAPI.Models;
using TrashCollectionAPI.ViewModel;

namespace TrashCollectionAPI.AutoMapperConfig
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AllowNullCollections = true;
                config.AllowNullDestinationValues = true;

                config.CreateMap<ColetaModel, ColetaViewModel>().ReverseMap();
                config.CreateMap<RotaModel, RotaViewModel>().ReverseMap();
                config.CreateMap<CaminhaoModel, CaminhaoViewModel>().ReverseMap();
                config.CreateMap<StatusModel, StatusViewModel>().ReverseMap();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
