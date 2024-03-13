using AutoMapper;
using FreeCourse.Shared.Dtos;
using FreeCourseServicesCatalog.API.Dtos;
using FreeCourseServicesCatalog.API.Models;
using FreeCourseServicesCatalog.API.Settings;
using MongoDB.Driver;

namespace FreeCourseServicesCatalog.API.Services
{
    internal class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _courseCollection = database.GetCollection<Course>(databaseSettings.CategoryCollectionName);

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

            _mapper = mapper;
        }

        public async Task<ResponseDto<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();

            if (courses.Any()) 
            {
            foreach(var course in courses) 
                {
                    course.Category = await _categoryCollection.Find<Category>(x=> x.Id == course.CategoryId).FirstAsync();
                }
            }
            else 
            {
                courses = new List<Course>();
            }

            var mappingCourseDto = _mapper.Map<List<CourseDto>>(courses);

            return ResponseDto<List<CourseDto>>.Success(mappingCourseDto, 200);
        }

        public async Task<ResponseDto<CourseDto>> GetByIdAsync(string id) 
        {
            var course = await _courseCollection.Find<Course>(x=> x.Id == id).FirstOrDefaultAsync();

            if(course == null) 
            {
                return ResponseDto<CourseDto>.Fail(new List<string> { "Course not Found" }, 404);
            }

            course.Category = await _categoryCollection.Find<Category>(x=> x.Id==course.CategoryId).FirstAsync();

            return ResponseDto<CourseDto>.Success(_mapper.Map<CourseDto>(course),200);
        }

        public async Task<ResponseDto<List<CourseDto>>> GetAllByUserIdAsync(string userId) 
        {
            var courses = await _courseCollection.Find<Course>(x => x.Id == userId).ToListAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            var mappingCourseDto = _mapper.Map<List<CourseDto>>(courses);

            return ResponseDto<List<CourseDto>>.Success(mappingCourseDto, 200);
        }

        public async Task<ResponseDto<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto) 
        {
            var newCourse = _mapper.Map<Course>(courseCreateDto);

            newCourse.CreationDate = DateTime.Now;

            await _courseCollection.InsertOneAsync(newCourse);

            return ResponseDto<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse),200);
        }

        public async Task<ResponseDto<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var updateCourse = _mapper.Map<Course>(courseUpdateDto);

            var result = await _courseCollection.FindOneAndReplaceAsync(x=> x.Id == courseUpdateDto.Id, updateCourse);

            if(result == null) 
            {
                return ResponseDto<NoContent>.Fail(new List<string> { "Course Not Found" }, 404);
            }

            return ResponseDto<NoContent>.Success(204);
        }

        public async Task<ResponseDto<NoContent>> DeleteAsync(string id) 
        {
            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);

            if(result.DeletedCount > 0)
            {
                return ResponseDto<NoContent>.Success(204);
            }
            else 
            {
                return ResponseDto<NoContent>.Fail(new List<string> { "Course not Found" }, 404);
            }
        }
    }
}


