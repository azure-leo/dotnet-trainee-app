global using TraineeAPI.Models;
using System.Text;
using TraineeAPI.Services.UserService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TraineeAPI.Data;
using TraineeAPI.Services.ArticleService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation  
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "JWT Token Authentication API",
        Description = "ASP.NET Core 3.1 Web API"
    });
    
    // To Enable authorization using Swagger (JWT)  
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()  
    {  
        Name = "Authorization",  
        Type = SecuritySchemeType.ApiKey,  
        Scheme = "Bearer",  
        BearerFormat = "JWT",  
        In = ParameterLocation.Header,  
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",  
    });  
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement  
    {  
        {  
            new OpenApiSecurityScheme  
            {  
                Reference = new OpenApiReference  
                {  
                    Type = ReferenceType.SecurityScheme,  
                    Id = "Bearer"  
                }  
            },  
            new string[] {}  
  
        }  
    });  
});
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IArticleService, ArticleService>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration.GetSection("AppSettings: Audience").Value,
            ValidIssuer = builder.Configuration.GetSection("AppSettings: Issuer").Value,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
        };
    });

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TraineeApp"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

