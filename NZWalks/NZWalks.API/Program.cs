
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Middlewares;
using NZWalks.API.Repositories;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace NZWalks.API
{
    public class AutoMapperProfile
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("Logs/NZWalks_log.txt",rollingInterval:RollingInterval.Minute)
                .MinimumLevel.Information()
                .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            builder.Services.AddControllers();
            builder.Services.AddHttpContextAccessor();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "NZ Walks Api", Version = "v1" });
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In=ParameterLocation.Header,
                    Type=SecuritySchemeType.ApiKey,
                    Scheme=JwtBearerDefaults.AuthenticationScheme
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference=new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id=JwtBearerDefaults.AuthenticationScheme
                        },
                        Scheme="Oauth2",
                        Name=JwtBearerDefaults.AuthenticationScheme,
                        In=ParameterLocation.Header
                    },
                    new List<string>()
                    }
                });                    
             });

            builder.Services.AddDbContext<NZWalksDBContext>(Options => 
            Options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksconnectionstrings")));
            builder.Services.AddDbContext<NZWalksAuthDBContext>(Options =>
            Options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksAuthConnectionString")));
            builder.Services.AddScoped<IRegionRepository,SQLRegionRepository>();
            builder.Services.AddScoped<IWalkRepository, SQLWalksRepository>();
            builder.Services.AddScoped<ITokenRepository, SQLTokenRepository>();
            builder.Services.AddScoped<IImageRepository, LocalImageRepository>();
            // builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfile).Assembly);

            builder.Services.AddIdentityCore<IdentityUser>().AddRoles<IdentityRole>().
            AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks").
            AddEntityFrameworkStores<NZWalksAuthDBContext>().AddDefaultTokenProviders();
            builder.Services.Configure<IdentityOptions>(Options =>
            {
                Options.Password.RequireDigit=false;
                Options.Password.RequireLowercase = false;
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequireUppercase = false;
                Options.Password.RequiredLength = 6;
                Options.Password.RequiredUniqueChars = 1;
            });

           builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(options=>options.TokenValidationParameters=new TokenValidationParameters
                {
                    ValidateIssuer=true,
                    ValidateAudience=true,
                    ValidateLifetime=true,
                    ValidateIssuerSigningKey=true,
                    ValidIssuer=builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey=new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
                });

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionhandlerMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
         
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider=new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Image")),
                RequestPath="/Image"
            });

            app.MapControllers();

            app.Run();
        }
    }
}
