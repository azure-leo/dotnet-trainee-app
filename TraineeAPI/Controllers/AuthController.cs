using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace TraineeAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IConfiguration _configuration;
    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    [HttpGet]
    public string Get()
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var issuer = _configuration.GetSection("AppSettings: Issuer").Value;
        var audience = _configuration.GetSection("AppSettings: Audience").Value;
        var jwtValidity = DateTime.Now.AddHours(Convert.ToDouble(_configuration.GetSection("AppSettings: ExpirationHours").Value));
        var token = new JwtSecurityToken(issuer,
            audience,
            expires: jwtValidity,
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }    
}