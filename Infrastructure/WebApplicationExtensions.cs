using System.Reflection;

namespace StopBulliesUserService.Infrastructure;

internal static class WebApplicationExtensions
{
    public static RouteGroupBuilder MapGroup(this WebApplication app, IEndpointGroupBase group)
    {
        string groupName = group.GetType().Name;

        return app
            .MapGroup($"/api/{groupName}")
            .WithGroupName(groupName)
            .WithTags(groupName)
            .WithOpenApi();
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        Type endpointGroupType = typeof(IEndpointGroupBase);

        var assembly = Assembly.GetExecutingAssembly();

        IEnumerable<Type> endpointGroupTypes = assembly.GetExportedTypes()
            .Where(t => t.IsSubclassOf(endpointGroupType));

        foreach (Type type in endpointGroupTypes)
        {
            if (Activator.CreateInstance(type) is IEndpointGroupBase instance)
            {
                instance.Map(app);
            }
        }

        return app;
    }
}
