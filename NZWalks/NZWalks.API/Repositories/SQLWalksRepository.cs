using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using System.Runtime.InteropServices;

namespace NZWalks.API.Repositories
{
    public class SQLWalksRepository : IWalkRepository
    {
        private readonly NZWalksDBContext dbcontext;

        public SQLWalksRepository(NZWalksDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbcontext.AddAsync(walk);
           await dbcontext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingwalk = await dbcontext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingwalk == null)
            {
                return null;
            }
            dbcontext.Walks.Remove(existingwalk);
            await dbcontext.SaveChangesAsync();
            return existingwalk;
        }

        public async Task<List<Walk>> GetAllAysnc(string? filterOn = null, string? filterQuery = null, 
            string? sortBy = null, bool IsAscending = true, int pageNumber = 1, int pageSize = 1000)
        {

            var wlk = dbcontext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            //filtering 

            if (string.IsNullOrWhiteSpace(filterOn) ==false && string.IsNullOrWhiteSpace(filterQuery) ==false)
            {
                if (filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    wlk = wlk.Where(x => x.name.Contains(filterQuery));
                }
                
            }

            //sorting

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {                 
                    wlk = IsAscending? wlk.OrderBy(x => x.name):wlk.OrderByDescending(x => x.name);
                }
                else if(sortBy.Equals("Length",StringComparison.OrdinalIgnoreCase))
                {
                    wlk = IsAscending ? wlk.OrderBy(x => x.lengthInKm) : wlk.OrderByDescending(x => x.lengthInKm);
                }

            }

            //Pagination

            var skipResults=(pageNumber-1)* pageSize;

            return await wlk.Skip(skipResults).Take(pageSize).ToListAsync();
            //return await wlk.ToListAsync();
            //return await dbcontext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }
     
        public async Task<Walk?> GetByIDAysnc(Guid id)
        {           
            return await dbcontext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id,Walk wlks)
        {
            var existingwalk = await dbcontext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingwalk == null)
            {
                return null;
            }          
            existingwalk.name = wlks.name;
            existingwalk.description = wlks.description;
            existingwalk.lengthInKm = wlks.lengthInKm;
            existingwalk.WalkImageUrl = wlks.WalkImageUrl;
            existingwalk.DifficultyId = wlks.DifficultyId;
            existingwalk.RegionId = wlks.RegionId;

            await dbcontext.SaveChangesAsync();
            return existingwalk;
        }
    }
}
