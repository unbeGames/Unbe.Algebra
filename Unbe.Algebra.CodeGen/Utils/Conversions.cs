using System;
using System.Collections.Generic;

namespace Unbe.Algebra.CodeGen {
  internal static partial class Utils {
    internal static readonly string[] conversionBaseTypes = new[] { 
      "float", "int", "bool", "uint", "double"
    };

    internal static readonly Dictionary<int, string[]> conversionVectorTypes = new() {
      { 2, new[] { "Bool2", "Float2", "Int2", "UInt2" } },
      { 3, new[] { "Bool3", "Float3", "Int3", "UInt3" } },
      { 4, new[] { "Bool4", "Float4", "Int4", "UInt4" } },
    };

    // source => destination : type
    private static readonly Dictionary<ValueTuple<string, string>, string> conversions = new() {
      // FLOAT
      { ("bool", "float"), "explicit" },
      { ("float", "float"), "implicit" },
      { ("int", "float"), "implicit" },
      { ("uint", "float"), "implicit" },
      { ("double", "float"), "explicit" },

      { ("Bool2", "Float2"), "explicit" },
      { ("Float2", "Float2"), "implicit" },
      { ("Int2", "Float2"), "implicit" },
      { ("UInt2", "Float2"), "implicit" },
      { ("Double2", "Float2"), "explicit" },

      { ("Bool3", "Float3"), "explicit" },
      { ("Float3", "Float3"), "implicit" },
      { ("Int3", "Float3"), "implicit" },
      { ("UInt3", "Float3"), "implicit" },
      { ("Double3", "Float3"), "explicit" },

      { ("Bool4", "Float4"), "explicit" },
      { ("Float4", "Float4"), "implicit" },
      { ("Int4", "Float4"), "implicit" },
      { ("UInt4", "Float4"), "implicit" },
      { ("Double4", "Float4"), "explicit" },

      { ("Vector128<float>", "float"), "implicit" },
      { ("Vector64<float>", "float"), "implicit" },

      // UINT
      { ("bool", "uint"), "explicit" },
      { ("uint", "uint"), "implicit" },
      { ("int", "uint"), "explicit" },
      { ("float", "uint"), "explicit" },
      { ("double", "uint"), "explicit" },

      { ("Bool2", "UInt2"), "explicit" },
      { ("UInt2", "UInt2"), "implicit" },
      { ("Int2", "UInt2"), "explicit" },
      { ("Float2", "UInt2"), "explicit" },
      { ("Double2", "UInt2"), "explicit" },

      { ("Bool3", "UInt3"), "explicit" },
      { ("UInt3", "UInt3"), "implicit" },
      { ("Int3", "UInt3"), "explicit" },
      { ("Float3", "UInt3"), "explicit" },
      { ("Double3", "UInt3"), "explicit" },

      { ("Bool4", "UInt4"), "explicit" },
      { ("UInt4", "UInt4"), "implicit" },
      { ("Int4", "UInt4"), "explicit" },
      { ("Float4", "UInt4"), "explicit" },
      { ("Double4", "UInt4"), "explicit" },

      { ("Vector128<uint>", "uint"), "implicit" },
      { ("Vector64<uint>", "uint"), "implicit" },

      // INT
      { ("bool", "int"), "explicit" },
      { ("uint", "int"), "explicit" },
      { ("int", "int"), "implicit" },
      { ("float", "int"), "explicit" },
      { ("double", "int"), "explicit" },

      { ("Bool2", "Int2"), "explicit" },
      { ("UInt2", "Int2"), "explicit" },
      { ("Int2", "Int2"), "implicit" },
      { ("Float2", "Int2"), "explicit" },
      { ("Double2", "Int2"), "explicit" },

      { ("Bool3", "Int3"), "explicit" },
      { ("UInt3", "Int3"), "explicit" },
      { ("Int3", "Int3"), "implicit" },
      { ("Float3", "Int3"), "explicit" },
      { ("Double3", "Int3"), "explicit" },

      { ("Bool4", "Int4"), "explicit" },
      { ("UInt4", "Int4"), "explicit" },
      { ("Int4", "Int4"), "implicit" },
      { ("Float4", "Int4"), "explicit" },
      { ("Double4", "Int4"), "explicit" },

      { ("Vector128<int>", "int"), "implicit" },
      { ("Vector64<int>", "int"), "implicit" },
    };

    internal static string ConvertOperator(string source, string destination) {
      return conversions[(source, destination)];
    }

    internal static string AddSuffixLyCap(string str) {
      return $"{str[0].ToString().ToUpper()}{str.Substring(1)}ly";
    }
  }
}
