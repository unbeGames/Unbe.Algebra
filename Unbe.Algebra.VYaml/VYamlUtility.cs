﻿using System;
using System.Linq;

namespace Unbe.Algebra.VYaml {
  public static class VYamlUtility {
    public static void Initialize() {
      var types = FindConverterTypes();
      foreach (var type in types) {
        RegisterConverters(type);
      }
    }

    /// <summary>
    /// Try to create the converter of specified type.
    /// </summary>
    /// <returns>The converter.</returns>
    /// <param name="type">Type.</param>
    private static void RegisterConverters(Type type) {
      try {
        var converter =  Activator.CreateInstance(type) as IYamlConverter;
        converter.Register();
      } catch (Exception exception) {
        Console.WriteLine("Can't create JsonConverter {0}:\n{1}", type, exception);
      }
    }

    /// <summary>
    /// Find all the valid converter types.
    /// </summary>
    /// <returns>The types.</returns>
    private static Type[] FindConverterTypes() {
      return AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany((dll) => dll.GetTypes())
        .Where((type) => typeof(IYamlConverter).IsAssignableFrom(type))
        .Where((type) => (!type.IsAbstract && !type.IsGenericTypeDefinition))
        .Where((type) => type.GetConstructor(new Type[0]) != null)
        .OrderBy((type) => type.Namespace != null && type.Namespace.StartsWith("Unbe.Algebra.VYaml"))
        .ToArray();
    }
  }
}