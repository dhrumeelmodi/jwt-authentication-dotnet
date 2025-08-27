using JWTAuth.DTO;
using JWTAuth.Models;
using JWTAuth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace JWTAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AuthController(UserManager<ApplicationUser> usermanager, 
            SignInManager<ApplicationUser> signInManager, 
            ITokenService tokenService)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };
            IdentityResult result = await _usermanager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            await _usermanager.AddToRoleAsync(user, "User");
            return Ok("Register Successfull!");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _usermanager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid username or password.");
            }
            var roles = await _usermanager.GetRolesAsync(user);
            var token = _tokenService.CreateToken(user, roles);
            return Ok(new
            {
                Token = token,
                //Username = user.UserName,
                //Roles = roles
            });
        }
    }
}
