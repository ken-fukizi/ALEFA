using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using UI.Areas.Identity;
using UI.Data;
using MudBlazor.Services;
using UI.Services;
using Common.Configuration;
using Microsoft.Extensions.Options;
using SharedKernel.Interfaces;
using static MassTransit.Logging.LogCategoryName;
using UI.Services.ServiceBus;
using MassTransit;
using UI.Services.ServiceBus.Observers;
using UI.Services.ServiceBus.Consumers;

namespace UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddScoped<IPatientDetailsService, PatientDetailsService>();
            builder.Services.AddMudServices();
            builder.Services.Configure<ServiceBusOptions>(builder.Configuration.GetSection("ServiceBusOptions"));
            builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<ServiceBusOptions>>().Value);

            #region ServiceBusConfigs
            builder.Services.AddTransient<IPublishObserver, PublishObserver>();
            builder.Services.AddTransient<IReceiveObserver, ReceiveObserver>();
            builder.Services.AddTransient<IBusObserver, BusObserver>();
            builder.Services.AddTransient<ISendObserver, SendObserver>();

            var sp = builder.Services.BuildServiceProvider();

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<AddPatientToLabTestCommandConsumer>();
                

                x.AddBus(context =>
                {
                    var massTransitBusOptions = context.Container.GetService<IOptions<ServiceBusOptions>>().Value;
                    var publishObserver = context.Container.GetService<IPublishObserver>();
                    var receiveObserver = context.Container.GetService<IReceiveObserver>();
                    var busObserver = context.Container.GetService<IBusObserver>();
                    var sendObserver = context.Container.GetService<ISendObserver>();

                    return ServiceBusConfigurator.ConfigureBus(context, massTransitBusOptions, publishObserver, receiveObserver, busObserver, sendObserver);
                }
                );
            });

            builder.Services.AddMassTransitHostedService();
            #endregion
            builder.Services.AddTransient<ApplicationBus>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}