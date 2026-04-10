using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDBContext dBContext;

        public LocalImageRepository(IWebHostEnvironment  webHostEnvironment ,IHttpContextAccessor httpContextAccessor, NZWalksDBContext dBContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dBContext = dBContext;
        }

        public async Task<Images> Upload(Images images)
        {
            var localfilepath = Path.Combine(webHostEnvironment.ContentRootPath,"Image",
               $"{images.FileName}{ images.FileExtension}");
            //Upload Image to Local path
            using var stream = new FileStream(localfilepath, FileMode.Create);  //Reads the filestram for this location
            await images.File.CopyToAsync(stream); //passing the stream for copy

            //https:localhost:1233/image/images.jpg

            var urlFithPath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}" +
                $"{httpContextAccessor.HttpContext.Request.PathBase}/Image/{images.FileName}{images.FileExtension}";
       
        images.Filepath = urlFithPath;

            //add the images to Image table

            await dBContext.Images.AddAsync(images);
            await dBContext.SaveChangesAsync();
            return images;

        }
    }
}
