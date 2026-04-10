namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string lengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId {  get; set; }

        //NavigationProperty..

        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}
