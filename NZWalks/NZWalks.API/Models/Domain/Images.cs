using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.Domain
{
    public class Images
    {
        public Guid id {  get; set; }

        [NotMapped]
        public IFormFile File {  get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }

        public string FileExtension {  get; set; }

        public long FilesizeInBytes {  get; set; }
        public string Filepath {  get; set; }
    }
}
