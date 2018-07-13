namespace Core.WebApi.Startup
{
    using Microsoft.Extensions.DependencyInjection;

    public static class SwaggerServices
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, string title, string version, string description, string termsOfService = "Terms of Service")
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc(version, new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = title,
                    Version = version,
                    Description = description,
                    TermsOfService = termsOfService
                });
            });

            return services;
        }
    }
}
