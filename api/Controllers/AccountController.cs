using FirstApp.DTO;
using FirstApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FirstApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _usermanager;
        public AccountController(UserManager<AppUser> usermanager)
        {
            _usermanager = usermanager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerdto)
        {
         
                AppUser user = new AppUser();
                user.Email = registerdto.Email;
                user.UserName = registerdto.UserName;
               IdentityResult result= await _usermanager.CreateAsync(user, registerdto.Pasword);
                if (result.Succeeded)
                {
                    return Ok("Success");
                }

            return BadRequest();
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO Logindto)
        {
            //check
            AppUser userfromdb = await _usermanager.FindByNameAsync(Logindto.UserName);
            bool found=  await _usermanager.CheckPasswordAsync(userfromdb, Logindto.Password);


            if (found)
            {

                List<Claim> UserClaims = new List<Claim>();
                UserClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, userfromdb.Id));
                UserClaims.Add(new Claim(ClaimTypes.Name, userfromdb.UserName));
                var roles = await _usermanager.GetRolesAsync(userfromdb);
                foreach(var role in roles)
                {
                    UserClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                SymmetricSecurityKey secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("kajfhieh38943y89y49ruriuwi2@bjhre3"));
                SigningCredentials signincredendial = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);

                //design token.....
                JwtSecurityToken mytoken = new JwtSecurityToken
                   (
                    issuer: "https://localhost:7236/",
                    claims: UserClaims,
                    expires:DateTime.Now.AddHours(1),
                    signingCredentials: signincredendial
                   );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                    expiration =DateTime.Now.AddHours(1)
                });
            }

            return NoContent();

           
        }
    }

 }
