namespace Service1.API
{
    using System;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Core.WebApi.Startup;
    using Domain.IntegrationEvents.Events;
    using Domain.IntegrationEvents.Settings;
    using Intrastructure.AutofacModules;
    using Microservices.EventBus.Abstractions;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddCustomMvc(this.Configuration)
                .AddBehaviorOptions(this.Configuration)
                .AddCustomOptions(this.Configuration)
                .AddIntegrationServices(this.Configuration)
                .AddEventBus(this.Configuration, this.Configuration["SubscriptionClientName"])
                .AddSwagger("Service 1 - API", "v1", "The Service 1 Microservice HTTP API. This is a microservice sample");

            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new ApplicationModule(this.Configuration["ConnectionString"]));

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var pathBase = this.Configuration["PATH_BASE"];

            if (!string.IsNullOrEmpty(pathBase))
            {
                loggerFactory.CreateLogger("init").LogDebug($"Using PATH BASE '{pathBase}'");
                app.UsePathBase(pathBase);
            }

            app.UseCors("CorsPolicy");

            app.UseMvcWithDefaultRoute();

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "Catalog.API V1");
                });

            this.ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<BasicIntegrationEvent, IIntegrationEventHandler<BasicIntegrationEvent>>();
        }
    }

    public static class StartupConfiguration
    {
        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<QueueSettings>(configuration);

            return services;
        }
    }
}
