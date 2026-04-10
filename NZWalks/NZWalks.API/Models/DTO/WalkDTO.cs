using NZWalks.API.Models.Domain;
using NZWalks.API.Models.NewFolder;

namespace NZWalks.API.Models.DTO
{
    public class WalkDTO
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string lengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        public RegionDTO Region { get; set; }

        public DifficultyDTO Difficulty { get; set; }



    }
}
