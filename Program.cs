using BookstoreAPI.Model;
using Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using IdentityDBContext.Services;
using IdentityDBContext;
using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddCookie(cfg => cfg.SlidingExpiration = true)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register the repository and context
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(ApplicationContext)); //for parameterless ApplicationContext()
builder.Services.AddScoped(typeof(DbContextOptions<ApplicationContext>)); //for arguments ApplicationContext(2DbContextOptions<ApplicationContext> options)

builder.Services.AddScoped<IUserRepositoryServices, UserRepositoryService>();
builder.Services.AddScoped(typeof(ApplicationIdentityDbContext)); //for parameterless ApplicationContext()
builder.Services.AddScoped(typeof(DbContextOptions<ApplicationIdentityDbContext>)); //for arguments ApplicationContext(2DbContextOptions<ApplicationContext> options)

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationIdentityDbContext>();

//builder.Services.AddScoped<SignInManager<ApplicationUser>>();
//builder.Services.AddScoped<UserManager<ApplicationUser>>();
//builder.Services.AddScoped<ApplicationUser>();

//add services
builder.Services.AddTransient<IBookServices, BooksServices>();
builder.Services.AddTransient<IPicturesServices, PicturesServices>();
builder.Services.AddTransient<IPictureBooksServices, PictureBooksServices>();
builder.Services.AddTransient<IAuthorServices, AuthorsServices>();
builder.Services.AddTransient<ILanguageServices, LanguageServices>();
builder.Services.AddTransient<IClassTypesServices, ClassTypesServices>();
builder.Services.AddTransient<ICartServices, CartServices>();
builder.Services.AddTransient<ICustomerServices, CustomerServices>();

//automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                          //.WithMethods("GET", "PUT", "DELETE", "POST", "PATCH"); //not really necessary when AllowAnyMethods is used.;
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("MyAllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.MapDelete("/api/delete/cart",
//[Authorize(AuthenticationSchemes = "Bearer")] async ([FromForm] int cartId, IUserRepositoryServices userRepositoryServices, ICustomerServices customerServices) =>
// {

//     return Results.Unauthorized();
// });

app.MapPost("/security/createToken",
[AllowAnonymous] async ([FromBody] UserModel userModel, IUserRepositoryServices userRepositoryServices, ICustomerServices customerServices) =>
{
    var identityResult = await userRepositoryServices.OnPostAsync(userModel.Email, userModel.Password);

    if (identityResult)
    {
        var customersInfo = await customerServices.GetCustomerByEmail(userModel.Email);

        if (customersInfo != null)
        {
            //---generate the token---
            var issuer = builder.Configuration["Jwt:Issuer"];
            var audience = builder.Configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim("LoginDate", DateTime.Now.ToString()),
                new Claim("Email", userModel.Email),
                new Claim("CustomersId", customersInfo.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userModel.Email),
                new Claim(JwtRegisteredClaimNames.Email, userModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);

            return Results.Ok(stringToken);
        }

        return Results.Unauthorized();
    }

    return Results.Unauthorized();
});

app.MapGet("/security/onLogout",
[AllowAnonymous] async (IUserRepositoryServices userRepositoryServices) =>
{
    return await userRepositoryServices.OnLogoutAsync();
});

//allow to access physical files
//app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"public/images")),
    RequestPath = new PathString("/public/images")
});

app.Run();
