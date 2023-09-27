using System;
using System.Collections.Generic;

namespace Unbe.Math.Generator {
  internal static class Utils {

    internal static readonly string[] shuffleNames = Enum.GetNames(typeof(Shuffle));


    internal static readonly Dictionary<string, string> typeAliases = new () {
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

    private static readonly HashSet<string> bitOpsTypes = new() {
      "int", "uint", "long", "ulong", "short", "ushort"
    };

    private static readonly HashSet<string> signedTypes = new() {
      "sbyte", "short", "int", "long", "float", "double"
    };

    private static readonly Dictionary<ValueTuple<string, int>, string> vectorPrefix = new() {
      { ("float", 4), "Vector128" },
      { ("uint", 4), "Vector128" },
      { ("int", 4), "Vector128" },
    };

    // source => destination : type
    private static readonly Dictionary<ValueTuple<string, string>, string> conversions = new() {
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

      { ("uint", "int"), "explicit" },
      { ("int", "int"), "implicit" },
      { ("bool", "int"), "explicit" },
      { ("float", "int"), "explicit" },
      { ("double", "int"), "explicit" },
      { ("Vector128<int>", "int"), "implicit" },
    };

    internal static bool SupportsBitOps(string type) {
      return bitOpsTypes.Contains(type);
    }

    internal static bool IsSigned(string type) {
      return signedTypes.Contains(type);
    }

    internal static string VectorPrefix(string type, int dimensions) {
      return vectorPrefix[(type, dimensions)];
    }

    internal static string ConvertOperator(string source, string destination) {
      return conversions[(source, destination)];
    }

    internal static string AddSuffLyCap(string str) {
      return $"{str[0].ToString().ToUpper()}{str.Substring(1)}ly";
    }
  }
}
