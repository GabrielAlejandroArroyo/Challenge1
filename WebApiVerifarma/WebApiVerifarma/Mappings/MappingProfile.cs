﻿using AutoMapper;
using WebApiVerifarma.DTOs.Farmacia;
using WebApiVeriframa.Models;

namespace WebApiVeriframa.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Farmacia, FarmaciaReadDTO>();
            CreateMap<FarmaciaCreateDTO, Farmacia>();
            CreateMap<FarmaciaUpdateDTO, Farmacia>();


        }
    }
}
