using AutoMapper;
using SmartMenu.Services.CouponAPI.Models;
using SmartMenu.Services.CouponAPI.Models.Dto;

namespace SmartMenu.Services.CouponAPI
{
    /// <summary>
    /// Provides configuration for AutoMapper mappings.
    /// </summary>
    public class MappingConfig
    {
        /// <summary>
        /// Registers the mappings between models and DTOs.
        /// </summary>
        /// <returns>The AutoMapper configuration.</returns>
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>();
                config.CreateMap<Coupon, CouponDto>();
            });
            return mappingConfig;
        }
    }
}
