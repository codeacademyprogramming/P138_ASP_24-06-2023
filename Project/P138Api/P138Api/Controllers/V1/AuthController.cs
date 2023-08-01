using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using P138Api.DTOs.AuthDTOs;
using P138Api.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace P138Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _config = config;
        }

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole("Admin"));
        //    await _roleManager.CreateAsync(new IdentityRole("Member"));

        //    return Ok();
        //}

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            AppUser appUser = _mapper.Map<AppUser>(registerDto);

            await _userManager.CreateAsync(appUser,registerDto.Password);
            await _userManager.AddToRoleAsync(appUser, "Admin");

            return Ok(appUser.Id);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(loginDto.Email);

            if (appUser == null) { return BadRequest(); }

            if (!await _userManager.CheckPasswordAsync(appUser,loginDto.Password))
            {
                return BadRequest();
            }

            var roles = await _userManager.GetRolesAsync(appUser);

            List<Claim> claims = new List<Claim> 
            {
                new Claim(ClaimTypes.Name,appUser.UserName),
                new Claim(ClaimTypes.Email,appUser.Email),
                new Claim(ClaimTypes.NameIdentifier,appUser.Id)
            };

            foreach (var role in roles)
            {
                Claim claim = new Claim(ClaimTypes.Role, role);
                claims.Add(claim);
            }

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                claims:claims, 
                signingCredentials:signingCredentials,
                expires : DateTime.UtcNow.AddHours(4)
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string token = handler.WriteToken(jwtSecurityToken);

            return Ok(token);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ReadToken()
        {
            string token = Request.Headers.Authorization.ToString().Split(' ')[1];

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = (JwtSecurityToken)jwtSecurityTokenHandler.ReadToken(token);

            List<Claim> claims = jwtSecurityToken.Claims.ToList();
            string email = claims.Find(c => c.Type == ClaimTypes.Role).Value;
            return Ok(email);
        }
    }
}
