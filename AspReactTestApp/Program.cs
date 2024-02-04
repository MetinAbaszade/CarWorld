using System.Text;
using AspReactTestApp.Data;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;
using AspReactTestApp.Services.CarService;
using AspReactTestApp.Services.FileService;
using AspReactTestApp.Services.AuthService;
using AspReactTestApp.Services.UserService;
using AspReactTestApp.Services.YearService;
using AspReactTestApp.Services.BrandService;
using AspReactTestApp.Services.ColorService;
using AspReactTestApp.Services.EmailService;
using AspReactTestApp.Services.ModelService;
using AspReactTestApp.Services.TokenService;
using AspReactTestApp.Services.RegionService;
using AspReactTestApp.Services.FeatureService;
using AspReactTestApp.Services.FueltypeService;
using AspReactTestApp.Services.CategoryService;
using AspReactTestApp.Data.DataAccess.Abstract;
using AspReactTestApp.Services.AutoSalonService;
using AspReactTestApp.Services.TransmissionService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AspReactTestApp.Data.DataAccess.Concrete.EntityFramework;
using System.Diagnostics;

namespace AspReactTestApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllersWithViews().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });

        builder.Services.AddAutoMapper(typeof(Program).Assembly);

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins("https://localhost:44461/") // Allow requests from any origin
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials(); // allow sending credentials (cookies);
            });
        });


        builder.Services.AddScoped<IAutoSalonRepository, EfAutoSalonRepository>();
        builder.Services.AddScoped<IBrandRepository, EfBrandRepository>();
        builder.Services.AddScoped<ICarRepository, EfCarRepository>();
        builder.Services.AddScoped<ICategoryRepository, EfCategoryRepository>();
        builder.Services.AddScoped<IColorRepository, EfColorRepository>();
        builder.Services.AddScoped<ICurrencyRepository, EfCurrencyRepository>();
        builder.Services.AddScoped<IFeatureRepository, EfFeatureRepository>();
        builder.Services.AddScoped<IFuelTypeRepository, EfFuelTypeRepository>();
        builder.Services.AddScoped<IModelRepository, EfModelRepository>();
        builder.Services.AddScoped<IRegionRepository, EfRegionRepository>();
        builder.Services.AddScoped<ITransmissionRepository, EfTransmissionRepository>();
        builder.Services.AddScoped<IYearRepository, EfYearRepository>();
        builder.Services.AddScoped<IUserRepository, EfUserRepository>();

        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IFileService, FileService>();
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<ITokenService, JwtTokenService>();
        builder.Services.AddScoped<IAutoSalonService, AutoSalonService>();
        builder.Services.AddScoped<IBrandService, BrandService>();
        builder.Services.AddScoped<ICarService, CarService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IColorService, ColorService>();
        builder.Services.AddScoped<IFeatureService, FeatureService>();
        builder.Services.AddScoped<IFuelTypeService, FuelTypeService>();
        builder.Services.AddScoped<IModelService, ModelService>();
        builder.Services.AddScoped<IRegionService, RegionService>();
        builder.Services.AddScoped<ITransmissionService, TransmissionService>();
        builder.Services.AddScoped<IYearService, YearService>();


        var assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        builder.Services.AddDbContext<AppDbContext>(options =>
          options.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=CarUniverse;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")); 


        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standart Authorization header using Bearer scheme (\"bearer {token}\")",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                               .GetBytes(builder.Configuration.GetSection("Appsettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("access_token"))
                        {
                            context.Token = context.Request.Cookies["access_token"];
                        }
                        return Task.CompletedTask;
                    }
                };
            });


        builder.Services.AddMemoryCache();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseHsts();
        }

        app.UseCors();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        // Place the UseAuthentication middleware here
        app.UseAuthentication();

        // Place the UseAuthorization middleware here
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}");

        app.MapFallbackToFile("index.html");

        app.Run();
    }
}