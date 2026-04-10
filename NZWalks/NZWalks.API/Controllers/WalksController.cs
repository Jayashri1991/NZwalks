using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.JSInterop;
using NZWalks.API.CustomActionFilter;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Models.NewFolder;
using NZWalks.API.Repositories;
using System.Data;
using System.Net;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository iwalkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.iwalkRepository = walkRepository;
        }
        //Create Walk
        //POST: api/walks

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDTO addwalkrequestDTO)
        {
            //map DTO to Domain model..
           
                var walkkdomainmodel = mapper.Map<Walk>(addwalkrequestDTO);
            await iwalkRepository.CreateAsync(walkkdomainmodel);

            //Map Domain model to DTO

            return Ok(mapper.Map<WalkDTO>(walkkdomainmodel));
           
        }
        //GET: http:localhost/api/walks?filteron=name&filterquery=Track%sortBy=Name&IsAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] 
        string? sortBy, [FromQuery] bool? IsAscending, [FromQuery] int pageNumber=1, [FromQuery] int pageSize = 1000)
        {
            //Get data from Database to Domainmodel
            var walkdomainmodel = await iwalkRepository.GetAllAysnc(filterOn, filterQuery, sortBy, IsAscending ?? true, pageNumber, pageSize);

            //Create an exception
            throw new Exception("This is a custom exception.");
            //map domain models to DTO 
            return Ok(mapper.Map<List<WalkDTO>>(walkdomainmodel));           
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid id)
        {
            //Get data from Database to Domainmodel

            var walkdomainmodel = await iwalkRepository.GetByIDAysnc(id);
            if (walkdomainmodel == null)
            {
                return NotFound();
            }
            //map domain models to DTO 

            return Ok(mapper.Map<WalkDTO>(walkdomainmodel));
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var domainmodel = await iwalkRepository.DeleteAsync(id);
            if (domainmodel == null)
            {
                return NotFound();            }

            return Ok(mapper.Map<WalkDTO>(domainmodel));           
        }
        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalksRequestDTO updateWalksRequest)
        {
            //Map  DTO to domain model
           

                  var walkdomainmodel = mapper.Map<Walk>(updateWalksRequest);

                var updatedwalk = await iwalkRepository.UpdateAsync(id, walkdomainmodel);
                if (updatedwalk == null)
                {
                    return NotFound();
                }
            //Map Domain model to DTO
            var regiondto = mapper.Map<WalkDTO>(updatedwalk);
            return Ok(regiondto);       
}
     }
}
