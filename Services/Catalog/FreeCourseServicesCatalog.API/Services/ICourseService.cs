using FreeCourse.Shared.Dtos;
using FreeCourseServicesCatalog.API.Dtos;

namespace FreeCourseServicesCatalog.API.Services
{
    public interface ICourseService
    {
        Task<ResponseDto<List<CourseDto>>> GetAllAsync();

        Task<ResponseDto<CourseDto>> GetByIdAsync(string id);

        Task<ResponseDto<List<CourseDto>>> GetAllByUserIdAsync(string userId);

        Task<ResponseDto<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);

        Task<ResponseDto<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto);

        Task<ResponseDto<NoContent>> DeleteAsync(string id);
    }
}
