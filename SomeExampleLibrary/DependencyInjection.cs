using AutoMapper.EquivalencyExpression;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SomeExampleLibrary {
    public static class DependencyInjection {

        public static IServiceCollection AddSomeExampleLibrary(this IServiceCollection services) {

            services.AddAutoMapper(
                (cfg) => {
                    cfg.AddCollectionMappers();
                },
                Assembly.GetExecutingAssembly()
            );

            services.AddScoped<SomeService>();

            return services;
        }

    }
}