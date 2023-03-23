using cotacao_api.Data;
using cotacao_api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace cotacao_api
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionstring = _configuration?.GetConnectionString("DefaultConnection");

            services.AddDbContext<DataContext>(opt => opt.UseMySql(connectionstring, ServerVersion.AutoDetect(connectionstring)));
            services.AddScoped<DataContext, DataContext>();
            services.AddTransient<Cotacao>();
            services.AddTransient<CotacaoItem>();


            services.AddCors(opt =>
            {
                var front_url = _configuration?.GetValue<string>("front_url");

                opt.AddDefaultPolicy(builder =>
                {
                    builder?.WithOrigins(front_url)?.AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API To Do List");
                });
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
