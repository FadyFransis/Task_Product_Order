using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using App.Common.Services.Hashing;
using App.Common.Services.Logger;
using App.Core.Entities;
using App.Core.Entities.Base;
using App.Core.Identity;
using App.Core.Interfaces.Repository;
using App.Core.Interfaces.Services;
using App.Core.Services;
using App.Infrastructure.Data;
using App.API.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using App.Common.Services.Mail;
using Microsoft.Extensions.Options;

namespace App.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "_myAllowSpecificOrigins",
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:44368", "http://localhost:45520",
                                                          "http://localhost:44395", "https://localhost:44395",
                                                          "https://localhost:44368", "https://echoadmin.sbtechnology.host", "https://echotrading.sbtechnology.host")
                                      .AllowAnyMethod().AllowAnyHeader();
                                  });
            });

            //services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            //{
            //    builder.WithOrigins("http://localhost:44395", "https://localhost:44395","*").AllowAnyMethod().AllowAnyHeader();
            //}));

            services.AddControllersWithViews();

            //services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddControllers();
            StartupHelper.RegisterDbContext(services, Configuration);
            RegisterCustomTypesInIoC(services);


            services.AddIdentity<AppUser, IdentityRole>
                (option =>
                {
                    // configure identity options
                    option.Password.RequireDigit = true;
                    option.Password.RequireLowercase = false;
                    option.Password.RequireUppercase = false;
                    option.Password.RequireNonAlphanumeric = false;
                    option.Password.RequiredLength = 6;
                    //option.Password.RequiredUniqueChars = 1;

                    // Lockout settings.
                    //option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    //option.Lockout.MaxFailedAccessAttempts = 5;
                    //option.Lockout.AllowedForNewUsers = true;

                    // User settings.
                    option.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    option.User.RequireUniqueEmail = false;

                })
                   .AddUserManager<ApplicationUserManager>()
                   .AddRoles<IdentityRole>()
                   .AddEntityFrameworkStores<AppDBContext>()
                   .AddDefaultTokenProviders();


            services.AddAuthentication()
              .AddCookie(options =>
              {
                  options.SlidingExpiration = true;
                  options.LoginPath = "/login";
                  options.LogoutPath = "/logout";
              })
              .AddJwtBearer(options =>
              {
                  options.RequireHttpsMetadata = false;
                  options.SaveToken = true;
                  options.TokenValidationParameters = new TokenValidationParameters()
                  {
                      RequireExpirationTime = false,
                      ValidateLifetime = true,
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidIssuer = Configuration["Tokens:Issuer"],
                      ValidAudience = Configuration["Tokens:Audience"],

                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),

                  };

              });


            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            //services.AddMvc().AddRazorOptions(options => options.AllowRecompilingViewsOnFileChange = true);
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    { Title = "Echo API Documentation ", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                // Configuring Swagger with JWT

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            services.AddDistributedMemoryCache();
            // set seesion time out 
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(356);
            });

            // set cookie timeout 


            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(356);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("_myAllowSpecificOrigins");

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    "areaRoute",
                    "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger(c => c.RouteTemplate = "swagger/{documentName}/swagger.json");
            app.UseSwaggerUI(c =>
            {
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Echo API V1");
            });

        }


        private static void RegisterCustomTypesInIoC(IServiceCollection services)
        {
            var asm = Assembly.Load("App.Core");
            var classes =
                asm.GetTypes().Where(p =>
                    p.Namespace != null && p.Namespace.StartsWith("App.Core.Entities") &&
                    p.IsClass
                    && (p.IsSubclassOf(typeof(BaseEntity)) ||
                        p.IsSubclassOf(typeof(BaseNameEntity)) ||
                        p.IsSubclassOf(typeof(CommonBaseNameBusinessEntity)))

                ).ToList();
            foreach (var c in classes)
            {
                // GenericRepository registration
                var iGenericRepositoryType = typeof(IGenericRepository<>).MakeGenericType(c);
                var genericRepositoryType = typeof(GenericEFRepository<>).MakeGenericType(c);
                services.AddScoped(iGenericRepositoryType, genericRepositoryType);

                // GenericService registration
                var iGenericServiceType = typeof(IGenericService<>).MakeGenericType(c);
                var genericServiceType = typeof(GenericService<>).MakeGenericType(c);
                services.AddScoped(iGenericServiceType, genericServiceType);
            }

            services.AddSingleton<Ilogger, LoggerService>();
            services.AddSingleton<IHashingService, HashingService>();

            services.AddScoped<IMailNotification, MailNotificationService>();

            services.AddScoped<IAppUserManagerService, AppUserManagerService>();
 
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();

        }
    }
}
