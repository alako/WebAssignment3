using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAssignment3.Models;
using WebAssignment3.ViewModels;

namespace WebAssignment3.Infrastructure
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<ComponentType, ComponentTypeViewModel>()
                .ForMember(c => c.Categories, opt => opt.Ignore())
                .ForMember(c => c.Components, opt => opt.Ignore())
                .ForMember(c => c.MultiSelectCategories, opt => opt.Ignore())
                .ForMember(c => c.MultiSelectListComponents, opt => opt.Ignore())
                .ForMember(c => c.SelectedCategories, opt => opt.Ignore())
                .ForMember(c => c.SelectedComponents, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Component, ComponentViewModel>()
                .ReverseMap();
        }
    }
}
