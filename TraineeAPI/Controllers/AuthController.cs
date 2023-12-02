using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TraineeAPI.Data;
using TraineeAPI.DTO;

namespace TraineeAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IConfiguration _configuration;
    public DataContext Context { get; set; }

    public AuthController(IConfiguration configuration, DataContext context)
    {
        _configuration = configuration;
        Context = context;
    }

    string CreateToken(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var issuer = _configuration.GetSection("AppSettings: Issuer").Value;
        var audience = _configuration.GetSection("AppSettings: Audience").Value;
        var jwtValidity =
            DateTime.Now.AddHours(Convert.ToDouble(_configuration.GetSection("AppSettings: ExpirationHours").Value));
        var token = new JwtSecurityToken(issuer,
            audience,
            new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim("username", user.Username),
            },
            expires: jwtValidity,
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [HttpPost("/login")]
    public async Task<ActionResult<string>> Login(LoginUserDTO user)
    {
        var foundUser = await Context.Users.Where(u => u.Username == user.Username).FirstOrDefaultAsync();
        if (foundUser == null)
        {
            return NotFound(new { message = "Not found" });
        }
        var valid = BCrypt.Net.BCrypt.Verify(user.Password, foundUser.Password);

       if (valid)
       {
           return Ok(new { token = CreateToken(foundUser) });
       }

       return BadRequest(new { message = "Invalid Password" });
    }

}