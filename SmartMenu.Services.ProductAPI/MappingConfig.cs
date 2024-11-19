using AutoMapper;
using SmartMenu.Services.ProductAPI.Models;
using SmartMenu.Services.ProductAPI.Models.Dto;

namespace SmartMenu.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
