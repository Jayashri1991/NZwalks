using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IImageRepository
    {
        Task<Images> Upload(Images images);
    }
}
