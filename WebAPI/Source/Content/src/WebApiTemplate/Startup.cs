using Microsoft.AspNetCore.CookiePolicy;

namespace WebApiTemplate
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            // Add Cookie Policy & HSTS
            AddCookiePolicyAndHsts(services);

            // Add OpenAPI (Swagger)
            AddOpenApi(services);

            // Add Memory Cache
            services.AddMemoryCache();
        }

        /// <summary>
        /// Add Cookie Policy & HSTS
        /// </summary>
        /// <param name="services"></param>
        private void AddCookiePolicyAndHsts(IServiceCollection services)
        {
            // Cookie
            services.AddCookiePolicy(options =>
            {
                options.HttpOnly = HttpOnlyPolicy.Always;
                options.Secure = CookieSecurePolicy.Always;
            });

            // HSTS
            services.AddHsts(options =>
            {
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });
        }

        /// <summary>
        /// Add OpenAPI (Swagger)
        /// </summary>
        /// <param name="services"></param>
        private void AddOpenApi(IServiceCollection services)
        {
            services.AddOpenApiDocument(config =>
            {
                config.DocumentName = "v1";
                config.Version = "1.0.0";
                config.Title = "WebApiTemplate";
                config.Description = "";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();

            // Use OpenAPI (Swagger)
            ConfigureOpenApi(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Use OpenAPI (Swagger)
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureOpenApi(IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi();
        }
    }
}
