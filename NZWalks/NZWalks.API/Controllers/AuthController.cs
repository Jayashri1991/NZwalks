using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Runtime.InteropServices;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        //POST: api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityuser = new IdentityUser
            {
                UserName = registerRequestDTO.Username,
                Email= registerRequestDTO.Username
            };
          var identityresult=  await userManager.CreateAsync(identityuser,registerRequestDTO.Password);
            if (identityresult.Succeeded)
            {
                //Add role to this User
                if(registerRequestDTO.Roles!=null&& registerRequestDTO.Roles.Any())
                {
                    identityresult = await userManager.AddToRolesAsync(identityuser, registerRequestDTO.Roles);                   
                    if (identityresult.Succeeded)
                    {
                        return Ok("User was Registered !Please login.");
                    }
                    }           
            }
            return BadRequest("Something went wrong!");
        }


        //POST: api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
           var user= await userManager.FindByEmailAsync(loginRequestDTO.Username);
            if(user!=null)
            {
                var checkpasswordresult = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
                if (checkpasswordresult)
                {

                    //get roles for this user
                    var roles= await userManager.GetRolesAsync(user);
                   if(roles!=null)
                    {
                        //Create Token
                        var JwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDTO
                        {
                            Jwttoken = JwtToken,
                        };
                        return Ok(response);
                    }                   

                } 
            }
            return BadRequest("Username Or password incorrect");
        }
    }
}
