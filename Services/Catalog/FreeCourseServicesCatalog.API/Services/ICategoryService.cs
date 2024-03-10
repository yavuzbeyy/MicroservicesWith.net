using AutoMapper;
using FreeCourse.Shared.Dtos;
using FreeCourseServicesCatalog.API.Dtos;
using FreeCourseServicesCatalog.API.Models;
using FreeCourseServicesCatalog.API.Settings;

namespace FreeCourseServicesCatalog.API.Services
{
    public interface ICategoryService
    {
        Task<ResponseDto<List<CategoryDto>>> GetAllAsync();

        Task<ResponseDto<CategoryDto>> CreateAsync(CategoryDto category);

        Task<ResponseDto<CategoryDto>> GetByIdAsync(string id);


    }
}
