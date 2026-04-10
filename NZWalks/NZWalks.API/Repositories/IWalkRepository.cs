using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using System.Runtime.InteropServices;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);

        Task<List<Walk>> GetAllAysnc(string? filterOn = null, string? filterQuery = null,string ? sortBy=null,bool IsAscending=true, int pageNumber = 1, int pageSize = 1000);

        Task<Walk?> GetByIDAysnc(Guid id);

        Task<Walk?> DeleteAsync(Guid id);

        Task<Walk?> UpdateAsync(Guid id, Walk walk);


    }
}
