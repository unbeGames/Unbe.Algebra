using System;
using System.Collections.Generic;

namespace Unbe.Math.Generator {
  internal static class Utils {

    internal static readonly string[] shuffle4Names = new string[] { "xxxx", "yxxx", "zxxx", "wxxx", "xyxx", "yyxx", "zyxx", "wyxx", "xzxx", "yzxx", "zzxx", "wzxx", "xwxx", "ywxx", "zwxx", "wwxx", "xxyx", "yxyx", "zxyx", "wxyx", "xyyx", "yyyx", "zyyx", "wyyx", "xzyx", "yzyx", "zzyx", "wzyx", "xwyx", "ywyx", "zwyx", "wwyx", "xxzx", "yxzx", "zxzx", "wxzx", "xyzx", "yyzx", "zyzx", "wyzx", "xzzx", "yzzx", "zzzx", "wzzx", "xwzx", "ywzx", "zwzx", "wwzx", "xxwx", "yxwx", "zxwx", "wxwx", "xywx", "yywx", "zywx", "wywx", "xzwx", "yzwx", "zzwx", "wzwx", "xwwx", "ywwx", "zwwx", "wwwx", "xxxy", "yxxy", "zxxy", "wxxy", "xyxy", "yyxy", "zyxy", "wyxy", "xzxy", "yzxy", "zzxy", "wzxy", "xwxy", "ywxy", "zwxy", "wwxy", "xxyy", "yxyy", "zxyy", "wxyy", "xyyy", "yyyy", "zyyy", "wyyy", "xzyy", "yzyy", "zzyy", "wzyy", "xwyy", "ywyy", "zwyy", "wwyy", "xxzy", "yxzy", "zxzy", "wxzy", "xyzy", "yyzy", "zyzy", "wyzy", "xzzy", "yzzy", "zzzy", "wzzy", "xwzy", "ywzy", "zwzy", "wwzy", "xxwy", "yxwy", "zxwy", "wxwy", "xywy", "yywy", "zywy", "wywy", "xzwy", "yzwy", "zzwy", "wzwy", "xwwy", "ywwy", "zwwy", "wwwy", "xxxz", "yxxz", "zxxz", "wxxz", "xyxz", "yyxz", "zyxz", "wyxz", "xzxz", "yzxz", "zzxz", "wzxz", "xwxz", "ywxz", "zwxz", "wwxz", "xxyz", "yxyz", "zxyz", "wxyz", "xyyz", "yyyz", "zyyz", "wyyz", "xzyz", "yzyz", "zzyz", "wzyz", "xwyz", "ywyz", "zwyz", "wwyz", "xxzz", "yxzz", "zxzz", "wxzz", "xyzz", "yyzz", "zyzz", "wyzz", "xzzz", "yzzz", "zzzz", "wzzz", "xwzz", "ywzz", "zwzz", "wwzz", "xxwz", "yxwz", "zxwz", "wxwz", "xywz", "yywz", "zywz", "wywz", "xzwz", "yzwz", "zzwz", "wzwz", "xwwz", "ywwz", "zwwz", "wwwz", "xxxw", "yxxw", "zxxw", "wxxw", "xyxw", "yyxw", "zyxw", "wyxw", "xzxw", "yzxw", "zzxw", "wzxw", "xwxw", "ywxw", "zwxw", "wwxw", "xxyw", "yxyw", "zxyw", "wxyw", "xyyw", "yyyw", "zyyw", "wyyw", "xzyw", "yzyw", "zzyw", "wzyw", "xwyw", "ywyw", "zwyw", "wwyw", "xxzw", "yxzw", "zxzw", "wxzw", "xyzw", "yyzw", "zyzw", "wyzw", "xzzw", "yzzw", "zzzw", "wzzw", "xwzw", "ywzw", "zwzw", "wwzw", "xxww", "yxww", "zxww", "wxww", "xyww", "yyww", "zyww", "wyww", "xzww", "yzww", "zzww", "wzww", "xwww", "ywww", "zwww", "wwww" };
    internal static readonly string[] shuffle3Names = new string[] { "xxx", "yxx", "zxx", "xyx", "yyx", "zyx", "xzx", "yzx", "zzx", "xxy", "yxy", "zxy", "xyy", "yyy", "zyy", "xzy", "yzy", "zzy", "xxz", "yxz", "zxz", "xyz", "yyz", "zyz", "xzz", "yzz", "zzz" };

    internal static readonly Dictionary<int, string[]> shuffleByDimension = new() {
      { 3, shuffle3Names }, { 4, shuffle4Names }
    };


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
      { ("float", 3), "Vector128" },
      { ("uint", 3), "Vector128" },
      { ("int", 3), "Vector128" },
      { ("float", 2), "Vector64" },
      { ("uint", 2), "Vector64" },
      { ("int", 2), "Vector64" },
    };

    // source => destination : type
    private static readonly Dictionary<ValueTuple<string, string>, string> conversions = new() {
      { ("float", "float"), "implicit" },
      { ("int", "float"), "implicit" },
      { ("bool", "float"), "explicit" },
      { ("uint", "float"), "implicit" },
      { ("double", "float"), "explicit" },
      { ("Vector128<float>", "float"), "implicit" },
      { ("Vector64<float>", "float"), "implicit" },
      
      { ("uint", "uint"), "implicit" },
      { ("int", "uint"), "explicit" },
      { ("bool", "uint"), "explicit" },
      { ("float", "uint"), "explicit" },
      { ("double", "uint"), "explicit" },
      { ("Vector128<uint>", "uint"), "implicit" },
      { ("Vector64<uint>", "uint"), "implicit" },

      { ("uint", "int"), "explicit" },
      { ("int", "int"), "implicit" },
      { ("bool", "int"), "explicit" },
      { ("float", "int"), "explicit" },
      { ("double", "int"), "explicit" },
      { ("Vector128<int>", "int"), "implicit" },
      { ("Vector64<int>", "int"), "implicit" },
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
