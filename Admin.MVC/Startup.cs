using System;
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
using Admin.MVC.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using App.Common.Services.Mail;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Admin.MVC.Services;
using Admin.MVC.Resources;

namespace Admin.MVC
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

            services.AddDistributedMemoryCache();
            // set seesion time out 
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });
            // set cookie timeout 
            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
            });

            //localization
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                    .AddViewLocalization()
                    .AddDataAnnotationsLocalization(options =>
                    {
                        options.DataAnnotationLocalizerProvider = (type, factory) =>
                        {
                            var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                            return factory.Create("SharedResource", assemblyName.Name);
                        };
                    });


            services.Configure<RequestLocalizationOptions>(options =>
            {
                CultureInfo EnglishCulture = new CultureInfo("en");
                CultureInfo ArabicCulture = new CultureInfo("ar")
                {
                    DateTimeFormat = EnglishCulture.DateTimeFormat
                };

                var supportedCultures = new[]
                {
                EnglishCulture,
                ArabicCulture
                };

                // State what the default culture for your application is. This will be used if no specific culture
                // can be determined for a given request.
                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");

                // You must explicitly state which cultures your application supports.
                // These are the cultures the app supports for formatting numbers, dates, etc.
                options.SupportedCultures = supportedCultures;

                // These are the cultures the app supports for UI strings, i.e. we have localized resources for.
                options.SupportedUICultures = supportedCultures;
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

            
            services.AddSingleton<CommonLocalizationService>();

           
            services.AddScoped<IAppUserManagerService, AppUserManagerService>();

          
            

            services.AddScoped<IProductService, ProductService>();
            ;
            services.AddScoped<IOrderService, OrderService>();
           
        }
    }
}
