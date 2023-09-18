using EasyNetQ;
using FL.Services.Comments.Managers;
using Microsoft.AspNetCore.Builder;

namespace FL.Services.Comments.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method is used to configure services (dependency injection).
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure services such as database connections, authentication, etc.
            // Example:
            // services.AddDbContext<MyDbContext>();
            // services.AddAuthentication(...);

            // Add MVC services with JSON serialization.
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            services
                .AddScoped(sp => RabbitHutch.CreateBus("host=localhost"))

                .AddScoped<ICommentManager, CommentManager>();

            // Configure Swagger
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOriginPolicy",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });
        }

        // This method is used to configure the application's request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Development-specific configuration goes here.
            }
            else
            {
                // Production-specific configuration goes here.
            }

            // Add middleware components to the request pipeline.
            app.UseHttpsRedirection();
            app.UseRouting();

            // Enable CORS (Cross-Origin Resource Sharing) if needed.
            app.UseCors(cors => cors
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials()
            );

            // Authentication and authorization middleware can be added here.
            // app.UseAuthentication();
            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Comments API");
            });
        }
    }
}
