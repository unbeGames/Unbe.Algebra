using System;
using System.Collections.Generic;
using System.Linq;

namespace Unbe.Algebra.CodeGen {
  [Flags]
  internal enum NumFlags {
    none = 0, bitOps = 4, bit8 = 8, bit16 = 16, bit32 = 32, bit64 = 64, 
    integralNum = 128, floatingPoint = 256, signed = 512, unsigned = 1024
  }

  internal static class Utils {
    static Utils() {
      GenerateShuffle4Inverse(shuffle4Names, 4);
      GenerateShuffle4Inverse(shuffle3Names, 3);
    }

    internal static readonly Dictionary<string, string> shuffleInverse = new();

    internal static readonly string[] shuffle4Names = new string[] { "xxxx", "yxxx", "zxxx", "wxxx", "xyxx", "yyxx", "zyxx", "wyxx", "xzxx", "yzxx", "zzxx", "wzxx", "xwxx", "ywxx", "zwxx", "wwxx", "xxyx", "yxyx", "zxyx", "wxyx", "xyyx", "yyyx", "zyyx", "wyyx", "xzyx", "yzyx", "zzyx", "wzyx", "xwyx", "ywyx", "zwyx", "wwyx", "xxzx", "yxzx", "zxzx", "wxzx", "xyzx", "yyzx", "zyzx", "wyzx", "xzzx", "yzzx", "zzzx", "wzzx", "xwzx", "ywzx", "zwzx", "wwzx", "xxwx", "yxwx", "zxwx", "wxwx", "xywx", "yywx", "zywx", "wywx", "xzwx", "yzwx", "zzwx", "wzwx", "xwwx", "ywwx", "zwwx", "wwwx", "xxxy", "yxxy", "zxxy", "wxxy", "xyxy", "yyxy", "zyxy", "wyxy", "xzxy", "yzxy", "zzxy", "wzxy", "xwxy", "ywxy", "zwxy", "wwxy", "xxyy", "yxyy", "zxyy", "wxyy", "xyyy", "yyyy", "zyyy", "wyyy", "xzyy", "yzyy", "zzyy", "wzyy", "xwyy", "ywyy", "zwyy", "wwyy", "xxzy", "yxzy", "zxzy", "wxzy", "xyzy", "yyzy", "zyzy", "wyzy", "xzzy", "yzzy", "zzzy", "wzzy", "xwzy", "ywzy", "zwzy", "wwzy", "xxwy", "yxwy", "zxwy", "wxwy", "xywy", "yywy", "zywy", "wywy", "xzwy", "yzwy", "zzwy", "wzwy", "xwwy", "ywwy", "zwwy", "wwwy", "xxxz", "yxxz", "zxxz", "wxxz", "xyxz", "yyxz", "zyxz", "wyxz", "xzxz", "yzxz", "zzxz", "wzxz", "xwxz", "ywxz", "zwxz", "wwxz", "xxyz", "yxyz", "zxyz", "wxyz", "xyyz", "yyyz", "zyyz", "wyyz", "xzyz", "yzyz", "zzyz", "wzyz", "xwyz", "ywyz", "zwyz", "wwyz", "xxzz", "yxzz", "zxzz", "wxzz", "xyzz", "yyzz", "zyzz", "wyzz", "xzzz", "yzzz", "zzzz", "wzzz", "xwzz", "ywzz", "zwzz", "wwzz", "xxwz", "yxwz", "zxwz", "wxwz", "xywz", "yywz", "zywz", "wywz", "xzwz", "yzwz", "zzwz", "wzwz", "xwwz", "ywwz", "zwwz", "wwwz", "xxxw", "yxxw", "zxxw", "wxxw", "xyxw", "yyxw", "zyxw", "wyxw", "xzxw", "yzxw", "zzxw", "wzxw", "xwxw", "ywxw", "zwxw", "wwxw", "xxyw", "yxyw", "zxyw", "wxyw", "xyyw", "yyyw", "zyyw", "wyyw", "xzyw", "yzyw", "zzyw", "wzyw", "xwyw", "ywyw", "zwyw", "wwyw", "xxzw", "yxzw", "zxzw", "wxzw", "xyzw", "yyzw", "zyzw", "wyzw", "xzzw", "yzzw", "zzzw", "wzzw", "xwzw", "ywzw", "zwzw", "wwzw", "xxww", "yxww", "zxww", "wxww", "xyww", "yyww", "zyww", "wyww", "xzww", "yzww", "zzww", "wzww", "xwww", "ywww", "zwww", "wwww" };
    internal static readonly string[] shuffle4To3Names = new string[] { "xxx", "yxx", "zxx", "wxx", "xyx", "yyx", "zyx", "wyx", "xzx", "yzx", "zzx", "wzx", "xwx", "ywx", "zwx", "wwx", "xxy", "yxy", "zxy", "wxy", "xyy", "yyy", "zyy", "wyy", "xzy", "yzy", "zzy", "wzy", "xwy", "ywy", "zwy", "wwy", "xxz", "yxz", "zxz", "wxz", "xyz", "yyz", "zyz", "wyz", "xzz", "yzz", "zzz", "wzz", "xwz", "ywz", "zwz", "wwz", "xxw", "yxw", "zxw", "wxw", "xyw", "yyw", "zyw", "wyw", "xzw", "yzw", "zzw", "wzw", "xww", "yww", "zww", "www" };
    internal static readonly string[] shuffle4To2Names = new string[] { "xx", "yx", "zx", "wx", "xy", "yy", "zy", "wy", "xz", "yz", "zz", "wz", "xw", "yw", "zw", "ww" };

    internal static readonly string[] shuffle3Names = new string[] { "xxx", "yxx", "zxx", "xyx", "yyx", "zyx", "xzx", "yzx", "zzx", "xxy", "yxy", "zxy", "xyy", "yyy", "zyy", "xzy", "yzy", "zzy", "xxz", "yxz", "zxz", "xyz", "yyz", "zyz", "xzz", "yzz", "zzz" };
    internal static readonly string[] shuffle3To2Names = new string[] { "xx", "yx", "zx", "xy", "yy", "zy", "xz", "yz", "zz" };

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

    internal static Dictionary<string, NumFlags> typeToFlags = new() {
      { "sbyte", NumFlags.integralNum | NumFlags.signed | NumFlags.bit8 },
      { "byte", NumFlags.integralNum | NumFlags.unsigned | NumFlags.bit8 },
      { "short", NumFlags.integralNum | NumFlags.signed | NumFlags.bit16 | NumFlags.bitOps },
      { "ushort", NumFlags.integralNum | NumFlags.unsigned | NumFlags.bit16 | NumFlags.bitOps },
      { "int", NumFlags.integralNum | NumFlags.signed | NumFlags.bit32 | NumFlags.bitOps },
      { "uint", NumFlags.integralNum | NumFlags.unsigned | NumFlags.bit32 | NumFlags.bitOps },
      { "float", NumFlags.floatingPoint | NumFlags.signed | NumFlags.bit32 },
      { "long", NumFlags.integralNum | NumFlags.signed | NumFlags.bit64 | NumFlags.bitOps },
      { "ulong", NumFlags.integralNum | NumFlags.unsigned | NumFlags.bit64 | NumFlags.bitOps },
      { "double", NumFlags.floatingPoint | NumFlags.signed | NumFlags.bit64 },
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

    internal static bool SupportsBitOps(NumFlags flags) {
      return (flags & NumFlags.bitOps) != 0;
    }

    internal static bool IsSigned(NumFlags flags) {
      return (flags & NumFlags.signed) != 0;
    }

    internal static bool IsIntegralNumeric(NumFlags flags) {
      return (flags & NumFlags.integralNum) != 0;
    }

    internal static bool IsBit16(NumFlags flags) {
      return (flags & NumFlags.bit16) != 0;
    }

    internal static bool IsBit32(NumFlags flags) {
      return (flags & NumFlags.bit32) != 0;
    }
    
    internal static bool IsBit64(NumFlags flags) {
      return (flags & NumFlags.bit64) != 0;
    }

    internal static bool IsFloatingPoint(NumFlags flags) {
      return (flags & NumFlags.floatingPoint) != 0;
    }

    internal static string VectorPrefix(string type, int dimensionX, int dimensionY) {
      return vectorPrefix[(type, dimensionX)];
    }

    internal static string ConvertOperator(string source, string destination) {
      return conversions[(source, destination)];
    }

    internal static string AddSuffLyCap(string str) {
      return $"{str[0].ToString().ToUpper()}{str.Substring(1)}ly";
    }

    private static void GenerateShuffle4Inverse(string[] shuffleNames, int uniqueCount) {
      for(int i = 0; i < shuffleNames.Length; i++) {
        var shuffle = shuffleNames[i];
        if(shuffle.Distinct().Count() == uniqueCount) {
          var inverse = GenerateShuffle4Inverse(shuffle);
          shuffleInverse.Add(shuffle, inverse);
        }
      }
    }

    private static readonly Dictionary<char, int> shufflePositions = new() { { 'x', 0 }, { 'y', 1 }, { 'z', 2 }, { 'w', 3 } };
    private static readonly char[] xyzw = new[] { 'x', 'y', 'z', 'w' };
    private static string GenerateShuffle4Inverse(string shuffle) {
      var arr = shuffle.ToCharArray();  
      char[] result = new char[arr.Length];

      // we basically doing
      // zxwy = xyzw
      // z = x, x = y, w = z, y = w
      // and then ordering it

      for(int i = 0; i < arr.Length; i++) {
        var next = arr[i];
        var letterPosition = shufflePositions[next];
        result[letterPosition] = xyzw[i];
      }
      return new string(result);
    }
  }
}
