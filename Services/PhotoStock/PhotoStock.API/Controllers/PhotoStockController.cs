using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoStock.API.Dtos;

namespace PhotoStock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoStockController : CustomBaseController
    {

        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo != null && photo.Length > 0)
            {
                var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photos", photo.FileName);

                using var stream = new FileStream(photoPath, FileMode.Create);
                {
                    await photo.CopyToAsync(stream, cancellationToken);

                    var returnPath = "photos/" + photo.FileName;

                    PhotoDto photoDto = new() { Url = returnPath };

                    return CreateActionResultInstance(ResponseDto<PhotoDto>.Success(photoDto, 200));
                }

            }

            return CreateActionResultInstance(ResponseDto<PhotoDto>.Fail(new List<string> { "Photo is empty" }, 400));
        }
        
        [HttpDelete]
        public IActionResult PhotoDelete(string photoUrl)
        {
            var photoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photos", photoUrl);

            if (System.IO.File.Exists(photoPath))
            {
                return CreateActionResultInstance(ResponseDto<NoContent>.Fail(new List<string> { "Photo not found" }, 404));
            }

            System.IO.File.Delete(photoPath);

            return CreateActionResultInstance(ResponseDto<NoContent>.Success(204));
        }

    }
}
