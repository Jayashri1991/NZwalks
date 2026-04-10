using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        //POST: /api/images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDTO request)
        {
            validateFileUpload(request);
            if(ModelState.IsValid)
            {
                //Convert DTO to Domain model

                var imagedomainmodel = new Images
                {
                    File= request.File,                   
                    FileExtension =Path.GetExtension(request.File.FileName),
                    FilesizeInBytes= request.File.Length,
                    FileName = request.File.FileName,
                    FileDescription = request.FileDescription

                };
                //use repository to Uploa the image

                await imageRepository.Upload(imagedomainmodel);
                return Ok(imagedomainmodel);
            }
            return BadRequest (ModelState);

        }

        private void validateFileUpload(ImageUploadDTO request)
        {
            var allowedextension = new string[] { ".jpg" ,".jpeg",".png"};
            if (!allowedextension.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file","Unsupported file extension");
            }
            if (request.File.Length>1045707)
            {
                ModelState.AddModelError("file", "File size more than 10 MB,Please upload a smaller size");
            }
        }
    }
}
