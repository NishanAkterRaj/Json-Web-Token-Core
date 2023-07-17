# Json Web Token Core


## Login and Athuntication 

## Insatall the Package
microsoft.aspnetcore.authentication.jwtbearer\6.0.18

## program.cs
``` 
/// first code add package and tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

///add authantication token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateLifetime = true,
             ValidIssuer = builder.Configuration["Jwt:Issuer"],
             ValidAudience = builder.Configuration["Jwt:Audience"],
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),


         };
         });

/// end token


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
///bind
app.UseAuthentication();
///end
app.UseAuthorization();

app.MapControllers();

app.Run();


```


### appsetting.json

```
 "Jwt": {
    "Issuer": "",
    "Audience": "",
    "Key":  ""
  },
```

###  Model Folder Create and create file User.cs
```
namespace Json_Web_Token_Core.Models
{
    public class Users
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
```

### Create a controller


```
using Json_Web_Token_Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Json_Web_Token_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration configuration){
        _config = configuration;
        }
        private Users AuthanticateUser(Users user)
        {
            Users _user = null;
            if (user.Username == "admin" && user.Password == "1234") { 
            
                _user = user;
            }
            return _user;

        }

        private string GenerateToken(Users users)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],null,expires:DateTime.Now.AddMinutes(1),
                signingCredentials : credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login (Users user)
        {
            IActionResult response = Unauthorized();
            var user_ = AuthanticateUser(user);
            if (user_ != null)
            {
                var token = GenerateToken(user_);
                response = Ok(new { token = token });
            }
            return response;
        }

        
    }
}

```



##### @Done Project Codding By

$ @Akter Hossain
$ @JsonWebToken
$ @Api


