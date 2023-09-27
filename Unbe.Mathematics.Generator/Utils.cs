using System;
using System.Collections.Generic;

namespace Unbe.Math.Generator {
  internal static class Utils {
    public static readonly string[] shuffleNames = Enum.GetNames(typeof(Shuffle));

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
      { ("uint", "float"), "implicit" },
      { ("double", "float"), "explicit" },
      { ("Vector128<float>", "float"), "implicit" },
      
      
      { ("uint", "uint"), "implicit" },
      { ("int", "uint"), "explicit" },
      { ("bool", "uint"), "explicit" },
      { ("float", "uint"), "explicit" },
      { ("double", "uint"), "explicit" },
      { ("Vector128<uint>", "uint"), "implicit" },
    };

    internal static string ConvertOperator(string source, string destination) {
      return conversions[(source, destination)];
    }

    internal static string AddSuffLyCap(string str) {
      return $"{str[0].ToString().ToUpper()}{str.Substring(1)}ly";
    }
  }
}
