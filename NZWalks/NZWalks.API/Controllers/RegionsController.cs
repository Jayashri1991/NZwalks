using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilter;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Models.NewFolder;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    //https:localhost:1234/api/Regions
    [Route("api/[controller]")]
    [ApiController]
  
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDBContext _dBContext;
        private readonly IRegionRepository iregionrepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(NZWalksDBContext dBContext,IRegionRepository Iregionrepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            _dBContext = dBContext;
            iregionrepository = Iregionrepository;
            this.mapper = mapper;
            this.logger = logger;
        }
        //Get All regions
        ///https://localhost:7062/api/Regions - This is restful api 
        [HttpGet]
        //[Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                throw new Exception("This is a custom exception.");
                //get data from database-domain models
                logger.LogInformation("Get All action method was invoked.");              

                var regionsdomain = await iregionrepository.GetAllAsync();
                logger.LogInformation($"Finish GetAll region request with data:{JsonSerializer.Serialize(regionsdomain)}");

                //map domain models to DTO         
                return Ok(mapper.Map<List<RegionDTO>>(regionsdomain));
            }
            catch(Exception ex)
            {
                logger.LogError(ex,ex.Message);
                throw;
            }       
          }
        //get Region ID wise

        [HttpGet]
        [Route("{id:Guid}")]
       // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetbyId([FromRoute] Guid id)
        {
            // var regions=_dBContext.Regions.Find(id); // get the reions Id wise
            //get data from database-domain models
            //var regiondomain = await _dBContext.Regions.FirstOrDefaultAsync(x=>x.ID==id);  // Linq
            var regiondomain = await iregionrepository.GetByIdAsync(id);
            if (regiondomain == null) 
            { 
                return NotFound();
            }

            //map/convert region domain model to region DTO..
            //var regiondto = new RegionDTO
            //{
            //    ID = regiondomain.ID,
            //    Name = regiondomain.Name,
            //    Code = regiondomain.Code,
            //    regionImageUrl = regiondomain.regionImageUrl
            //};
            //map domain models to DTO         
            return Ok(mapper.Map<RegionDTO>(regiondomain));

            //Return back DTO to Client
         
        }
        //Post to Create New region
        //Post: https:localhost/portnum/api/region
        [HttpPost]
        [ValidateModel]
       // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addregionrequestDTO)
        {
            //Map or Convert  DTO to Domain model 

            //var regiondomainmodel = new Region
            //{
            //    Code = addregionrequestDTO.Code,
            //    Name = addregionrequestDTO.Name,
            //    regionImageUrl = addregionrequestDTO.regionImageUrl
            //};
            //Use domain model to create

            //await _dBContext.Regions.AddAsync(regiondomainmodel);
            //await _dBContext.SaveChangesAsync();
            
            var regiondomainmodel = mapper.Map<Region>(addregionrequestDTO);

            regiondomainmodel = await iregionrepository.CreateAsync(regiondomainmodel);


            //Map domain model  back to DTO

            //var regionDTO= new RegionDTO
            //{
            //    ID= regiondomainmodel.ID,
            //    Code = regiondomainmodel.Code,
            //    Name = regiondomainmodel.Name,
            //    regionImageUrl = regiondomainmodel.regionImageUrl
            //};
         var regionDTO= mapper.Map<RegionDTO>(regiondomainmodel);

            return CreatedAtAction(nameof(GetbyId), new {id= regionDTO.ID}, regionDTO);
       
        }
        //PUT : Update region
        //PUT:https:localhost/portnum/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
       // [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateregionrequestDTO)
        {
            //var regiondomainmodel=  await _dBContext.Regions.FirstOrDefaultAsync(x => x.ID == id);
            //  if (regiondomainmodel==null)
            //  {
            //      return NotFound();
            //  }
            //Map  DTO to domain model
           

                var regiondomainmodel = mapper.Map<Region>(updateregionrequestDTO);

                var updatedRegion = await iregionrepository.UpdateAsync(id, regiondomainmodel);
                // Handle not found

                ////Map  DTO to domain model
                //regiondomainmodel.Code = updateregionrequestDTO.Code;
                //regiondomainmodel.Name = updateregionrequestDTO.Name;
                //regiondomainmodel.regionImageUrl = updateregionrequestDTO.regionImageUrl;
                //await _dBContext.SaveChangesAsync();


                //Convert domain model to DTO

                //var regiondto = new RegionDTO
                //{
                //    ID=regiondomainmodel.ID,
                //    Code = regiondomainmodel.Code,
                //    Name = regiondomainmodel.Name,
                //    regionImageUrl = regiondomainmodel.regionImageUrl
                //};
                var regiondto = mapper.Map<RegionDTO>(updatedRegion);
                return Ok(regiondto);
           
        }


        [HttpDelete]
        [Route("{id:Guid}")]
       // [Authorize(Roles = "Writer,Reader")]
        //Delete Region
        //Delete : https:localhost:portnumber/api/regions/{id}

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
           var regionmodel= await iregionrepository.DeleteAsync(id);
            if (regionmodel==null)
            {
                return NotFound();
            }
           //_dBContext.Regions.Remove(regionmodel);
           //await _dBContext.SaveChangesAsync();

            //return the deleted region back

            //var regiondto = new RegionDTO
            //{
            //    ID = regionmodel.ID,
            //    Code = regionmodel.Code,
            //    Name = regionmodel.Name,
            //    regionImageUrl = regionmodel.regionImageUrl
            //};

            return Ok(mapper.Map<RegionDTO>(regionmodel));
        }

    }
}
