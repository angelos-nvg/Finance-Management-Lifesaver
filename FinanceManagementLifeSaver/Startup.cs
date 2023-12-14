using FinanceManagementLifesaver.Data;
using FinanceManagementLifesaver.Interfaces;
using FinanceManagementLifesaver.Services;
using Hangfire.SqlServer;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FinanceManagementLifesaver.BackgroundWorker;

namespace FinanceManagementLifesaver
{
    public class Startup
    {
        private IBackgroundWorkerService _backgroundWorkerService;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddCors();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        //builder.WithOrigins("http://vmentvs05")
                        builder.AllowAnyOrigin()
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
            JobStorage.Current = new SqlServerStorage(Configuration.GetConnectionString("DefaultConnection"));
            services.AddHangfireServer();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IBackgroundWorkerService, BackgroundWorkerService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IInvestmentService, InvestmentService>();
            //services.AddScoped<IContactService, ContactService>();
            var serviceProvider = services.BuildServiceProvider();
            _backgroundWorkerService = serviceProvider.GetService<IBackgroundWorkerService>();
            RecurringJob.AddOrUpdate("ExecuteMonthlyClosing", () => _backgroundWorkerService.ExecuteMonthlyClosing(), "0 2 * * *");
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Implement Swagger UI",
                    Description = "A simple example to Implement Swagger UI",
                });
            });


            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.

            app.UseHangfireDashboard();

            app.UseStaticFiles();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.InjectStylesheet("/swagger-ui/SwaggerStyle.css");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
