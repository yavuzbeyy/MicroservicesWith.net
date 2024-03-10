using AutoMapper;
using FreeCourseServicesCatalog.API.Dtos;
using FreeCourseServicesCatalog.API.Models;
using System;

namespace FreeCourseServicesCatalog.API.Dtos.Mapping{ 
public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Feature, FeatureDto>().ReverseMap();

        CreateMap<Course, CourseCreateDto>().ReverseMap();
        CreateMap<Course, CourseUpdateDto>().ReverseMap();

    }
}
}
