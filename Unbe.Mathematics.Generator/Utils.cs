using System;
using System.Collections.Generic;

namespace Unbe.Math.Generator {
  internal static class Utils {
    internal static Dictionary<string, string> typeAliases = new () {
      { "SByte", "sbyte" },
      { "Byte", "byte" },
      { "Int16", "short" },
      { "UInt16", "ushort" },
      { "Int32", "int" },
      { "UInt32", "uint" },
      { "Int64", "long" },
      { "UInt64", "ulong" },
      { "Single", "float" },
      { "Double", "double" },
      { "Boolean", "bool" },
    };

    // source => destination : type
    internal static Dictionary<ValueTuple<string, string>, string> conversions = new() {
      { ("float", "float"), "implicit" },
      { ("int", "float"), "implicit" },
      { ("bool", "float"), "explicit" },
      
      
      { ("uint", "uint"), "implicit" },
      { ("int", "uint"), "explicit" },
      { ("bool", "uint"), "explicit" },
    };

    internal static string ConvertOperator(string source, string destination) {
      return conversions[(source, destination)];
    }

    internal static string AddSuffLyCap(string str) {
      return $"{str[0].ToString().ToUpper()}{str.Substring(1)}ly";
    }
  }
}
