using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace QuizPractice.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddValidatorsFromAssembly(assembly);

        // Every XxxService is registered against its IXxxService, so adding a service
        // needs no line here (and no merge conflict on this file).
        foreach (var type in assembly.GetTypes()
                     .Where(t => t is { IsClass: true, IsAbstract: false } && t.Name.EndsWith("Service")))
        {
            var contract = type.GetInterfaces().SingleOrDefault(i => i.Name == "I" + type.Name)
                ?? throw new InvalidOperationException($"{type.Name} does not implement I{type.Name}.");
            services.AddScoped(contract, type);
        }

        return services;
    }
}
