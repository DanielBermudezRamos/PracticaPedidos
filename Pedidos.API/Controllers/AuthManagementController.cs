using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pedidos.BL.Data.Authentication;
using Pedidos.BL.DTOs.Requests;
using Pedidos.BL.DTOs.Responses;
using System.Collections.Generic;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pedidos.BL.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace Pedidos.API.Controllers
{
    [Route("api/[controller]")] // api/authManagement
    [ApiController]
    [AllowAnonymous]
    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly JwtConfig _jwtConfig;

        
        public AuthManagementController(UserManager<ApplicationUser> userManager, 
                                        RoleManager<IdentityRole> roleManager, 
                                        IConfiguration configuration,
                                        IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this._configuration = configuration;
            this._jwtConfig = optionsMonitor.CurrentValue;

        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto Register)
        {
            if (ModelState.IsValid)
            {
                var userExist = await userManager.FindByNameAsync(Register.Usuario);
                if(userExist != null)
                    return BadRequest(new RegistrationResponse()
                    {
                        Errores = new List<string>() { "Este Email ya está Registrado" },
                        Exitoso = false
                    });
                ApplicationUser user = new()
                {
                    Email = Register.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = Register.Usuario
                };
                var isCreated = await userManager.CreateAsync(user, Register.Clave);
                if (isCreated.Succeeded)
                {
                    var jwtToken = GenerateJwtToken(user);

                    return Ok(new RegistrationResponse()
                    {
                        Exitoso = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errores = isCreated.Errors.Select(x => x.Description).ToList(),
                        Exitoso = false
                    });
                }
            }
            else
            {
                return BadRequest(new RegistrationResponse()
                {
                    Errores = new List<string>() { "Invalid payload" },
                    Exitoso = false
                });
            }
        }

        /// <summary>
        /// Método encargado de la Autenticación
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns>Objeto con resultado y token para la autorización</returns>
        /// <return>Objeto con resultado y lista de errores</return>
        [HttpPost]
        //    [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userLogin)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await userManager.FindByEmailAsync(userLogin.Email);
                if (existingUser != null && await userManager.CheckPasswordAsync(existingUser, userLogin.Clave))
                {
                    var useRoles = await userManager.GetRolesAsync(existingUser);
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, existingUser.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                    var authSigninKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secreta"]));
                    var token =  GenerateJwtToken(existingUser);

                    return Ok(new RegistrationResponse()
                    {
                        Exitoso = true,
                        Token = token
                    });
                }
                return BadRequest(new RegistrationResponse()
                {
                    Errores = new List<string>() { "Invalid login request" },
                    Exitoso = false
                });
                //return Unauthorized();
            }
            return BadRequest(new RegistrationResponse()
            {
                Errores = new List<string>() { "Invalid payload" },
                Exitoso = false
            });
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:Secreta"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim("Id", user.Id),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }


    //public class AuthManagementController : ControllerBase
    //{
    //    private readonly UserManager<IdentityUser> _userManager;
    //    private readonly JwtConfig _jwtConfig;

    //    public AuthManagementController(
    //        UserManager<IdentityUser> userManager,
    //        IOptionsMonitor<JwtConfig> optionsMonitor)
    //    {
    //        _userManager = userManager;
    //        _jwtConfig = optionsMonitor.CurrentValue;
    //    }

    //    [HttpPost]
    //    [Route("Register")]
    //    public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
    //    {
    //        if(ModelState.IsValid)
    //        {
    //            // We can utilise the model
    //            var existingUser = await _userManager.FindByEmailAsync(user.Email);

    //            if(existingUser != null)
    //            {
    //                return BadRequest(new RegistrationResponse(){
    //                        Errores = new List<string>() {
    //                            "Email already in use"
    //                        },
    //                        Exitoso = false
    //                });
    //            }

    //            var newUser = new IdentityUser() { Email = user.Email, UserName = user.Usuario};
    //            var isCreated = await _userManager.CreateAsync(newUser, user.Clave);
    //            if(isCreated.Succeeded)
    //            {
    //               var jwtToken =  GenerateJwtToken( newUser);

    //               return Ok(new RegistrationResponse() {
    //                   Exitoso = true,
    //                   Token = jwtToken
    //               });
    //            } else {
    //                return BadRequest(new RegistrationResponse(){
    //                        Errores = isCreated.Errors.Select(x => x.Description).ToList(),
    //                        Exitoso = false
    //                });
    //            }
    //        }

    //        return BadRequest(new RegistrationResponse(){
    //                Errores = new List<string>() {
    //                    "Invalid payload"
    //                },
    //                Exitoso = false
    //        });
    //    }

    //    [HttpPost]
    //    [Route("Login")]
    //    public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
    //    {
    //        if(ModelState.IsValid)
    //        {
    //            var existingUser = await _userManager.FindByEmailAsync(user.Email);

    //            if(existingUser == null) {
    //                    return BadRequest(new RegistrationResponse(){
    //                        Errores = new List<string>() {
    //                            "Invalid login request"
    //                        },
    //                        Exitoso = false
    //                });
    //            }

    //            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Clave);

    //            if(!isCorrect) {
    //                  return BadRequest(new RegistrationResponse(){
    //                        Errores = new List<string>() {
    //                            "Invalid login request"
    //                        },
    //                        Exitoso = false
    //                });
    //            }

    //            var jwtToken  =GenerateJwtToken(existingUser);

    //            return Ok(new RegistrationResponse() {
    //                Exitoso = true,
    //                Token = jwtToken
    //            });
    //        }

    //        return BadRequest(new RegistrationResponse(){
    //                Errores = new List<string>() {
    //                    "Invalid payload"
    //                },
    //                Exitoso = false
    //        });
    //    }

    //    private string GenerateJwtToken(IdentityUser user)
    //    {
    //        var jwtTokenHandler = new JwtSecurityTokenHandler();

    //        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secreta);

    //        var tokenDescriptor = new SecurityTokenDescriptor
    //        {
    //            Subject = new ClaimsIdentity(new []
    //            {
    //                new Claim("Id", user.Id), 
    //                new Claim(JwtRegisteredClaimNames.Email, user.Email),
    //                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
    //                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    //            }),
    //            Expires = DateTime.UtcNow.AddHours(6),
    //            SigningCredentials = 
    //                new SigningCredentials(
    //                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //        };

    //        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
    //        var jwtToken = jwtTokenHandler.WriteToken(token);

    //        return jwtToken;
    //    }
    //}

}