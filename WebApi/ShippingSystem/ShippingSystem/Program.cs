
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShippingSystem.Models;
using ShippingSystem.Services;
using System.Text;

namespace ShippingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ShippingContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("con"))
                                  .UseLazyLoadingProxies()
            );

            builder.Services.AddIdentity<ApplicationUser,Group>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 5;
            })
                .AddEntityFrameworkStores<ShippingContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options => {
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
            });

            builder.Services.AddScoped<IAccountControllerService, AccountControllerService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            //> Ignore reference loops in the JSON serialization
            builder.Services.AddControllers().AddNewtonsoftJson(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            //> Swagger Configuration
            builder.Services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Shipping System Web APIs Documentation",
                    Version = "v1",
                    Description = "This Awesome Web Api generated by men3m helps you to use all the services provided :)",
                    Contact = new OpenApiContact
                    {
                        Name = "ITI Team 1",
                        Email = "abdelmonemanwr7777@gmail.com",
                        Url = new Uri("https://github.com/abdelmonemanwr")
                    },
                });
                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "shippingXmlComments.xml"));
                options.EnableAnnotations();
                #region add security definition
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization Example: 'Bearer'",
                    Name = "Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement(){{
                    new OpenApiSecurityScheme{
                        Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme },
                        In = ParameterLocation.Header,
                        Scheme = "outh2",
                        Name = "Bearer",
                    },
                    new List<string>()

                }});
                #endregion
            });

            //> Add CORS Policies
            string Policies = "";
            builder.Services.AddCors(options => {
                options.AddPolicy(Policies,
                builder => {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            //> Add JWT Authentication
            #region jwt use default schema
            var JwtSettings = builder.Configuration.GetSection("JwtSettings");

            builder.Services.AddAuthentication(options => {
                //This property sets the default scheme that will be used for authenticating the user's identity.
                //When a request is received, ASP.NET Core checks for authentication credentials using the scheme specified here.
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                //This property sets the default scheme that will be used to challenge unauthorized requests.
                //When a request is received without authentication credentials or with invalid credentials, ASP.NET Core needs to send a challenge back to the client, indicating that authentication is required.
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                // options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o => {
                o.SaveToken = true;
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = JwtSettings["ValidAudience"],
                    ValidIssuer = JwtSettings["ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.GetSection("securityKey").Value!))
                };
            });
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //> middleware to use the authentication
            app.UseAuthentication();

            app.UseAuthorization();

            //> middleware to use the cors policy
            app.UseCors(Policies);

            app.MapControllers();

            app.Run();
        }
    }
}
