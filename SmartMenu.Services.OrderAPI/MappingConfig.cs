﻿using AutoMapper;
using SmartMenu.Services.OrderAPI.Models;
using SmartMenu.Services.OrderAPI.Models.Dto;

namespace SmartMenu.Services.OrderAPI
{
    /// <summary>
    /// Configuration class for setting up AutoMapper mappings.
    /// </summary>
    public class MappingConfig
    {
        /// <summary>
        /// Registers the mappings for AutoMapper.
        /// </summary>
        /// <returns>The AutoMapper configuration.</returns>
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<OrderHeaderDto, CartHeaderDto>()
                    .ForMember(dest => dest.CartTotal, u => u.MapFrom(src => src.OrderTotal))
                    .ReverseMap();

                config.CreateMap<CartDetailsDto, OrderDetailsDto>()
                    .ForMember(dest => dest.ProductName, u => u.MapFrom(src => src.Product.Name))
                    .ForMember(dest => dest.Price, u => u.MapFrom(src => src.Product.Price));

                config.CreateMap<OrderDetailsDto, CartDetailsDto>();

                config.CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
                config.CreateMap<OrderDetailsDto, OrderDetails>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
