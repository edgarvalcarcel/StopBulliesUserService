using System.Reflection;
using Ardalis.GuardClauses;
namespace PlanifyIdentity.Infrastructure;

public static class MethodInfoExtensions
{
    public static bool IsAnonymous(this MethodInfo method)
    {
        char[] invalidChars = ['<', '>'];
        return method.Name.Any(invalidChars.Contains);
    }
}
