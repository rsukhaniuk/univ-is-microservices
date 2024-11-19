using AutoMapper;
using SmartMenu.Services.RecipeAPI.Models;
using SmartMenu.Services.RecipeAPI.Models.Dto;

namespace SmartMenu.Services.RecipeAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<RecipeDto, Recipe>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
