using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class ReflectionHelper
{
    public static List<Type> FindImplementations<TInterface>()
    {
        // Get the current assembly or specify the assembly you want to scan
        Assembly assembly = Assembly.GetExecutingAssembly();

        // Find all types that are class, not abstract and implement the specified interface
        var types = assembly.GetTypes()
                            .Where(t => t.IsClass && !t.IsAbstract && typeof(TInterface).IsAssignableFrom(t))
                            .ToList();

        return types;
    }
}
