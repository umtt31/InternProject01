﻿using AspNetCoreMvc004.Models;
using AspNetCoreMvc004.ViewModels;
using AutoMapper;

namespace AspNetCoreMvc004.Mapping
{
    public class ViewModelMapping : Profile
    {
        public ViewModelMapping()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}