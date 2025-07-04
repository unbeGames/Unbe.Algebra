﻿using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Unbe.Algebra {
  public static partial class Vector256Ext {
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Reminder(in Vector256<double> left, in Vector256<double> right) {
      var n = left / right;
      n = n.Truncate();

      return left - n * right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<long> Reminder(in Vector256<long> left, in Vector256<long> right) {
      var n = left / right;

      return left - n * right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<ulong> Reminder(in Vector256<ulong> left, in Vector256<ulong> right) {
      var n = left / right;

      return left - n * right;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Shuffle(in Vector256<double> vector, byte control) {
      if (Avx2.IsSupported) {
        return Avx2.Permute4x64(vector, control);
      }

      return Shuffle(vector, vector, control);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<long> Shuffle(in Vector256<long> left, in Vector256<long> right, byte control) {
      return Shuffle(left.AsDouble(), right.AsDouble(), control).AsInt64();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<ulong> Shuffle(in Vector256<ulong> left, in Vector256<ulong> right, byte control) {
      return Shuffle(left.AsDouble(), right.AsDouble(), control).AsUInt64();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Shuffle(in Vector256<double> left, in Vector256<double> right, byte control) {
      if (Avx.IsSupported) {
        return Avx.Shuffle(left, right, control);
      }

      return ShuffleSoftware(left, right, control);      
    }

    internal static Vector256<T> ShuffleSoftware<T>(in Vector256<T> left, in Vector256<T> right, byte control) where T : unmanaged {
      const byte e0Mask = 0b_0000_0011, e1Mask = 0b_0000_1100, e2Mask = 0b_0011_0000, e3Mask = 0b_1100_0000;

      int e0Selector = control & e0Mask;
      var e0 = left.GetElement(e0Selector);

      int e1Selector = (control & e1Mask) >> 2;
      var e1 = left.GetElement(e1Selector);

      int e2Selector = (control & e2Mask) >> 4;
      var e2 = right.GetElement(e2Selector);

      int e3Selector = (control & e3Mask) >> 6;
      var e3 = right.GetElement(e3Selector);

      ReadOnlySpan<T> array = stackalloc T[] { e0, e1, e2, e3 };

      return Vector256.Create(array);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Round(Vector256<double> vector) {
      if (Avx.IsSupported) {
        return Avx.RoundToNearestInteger(vector);
      }

      if (Sse41.IsSupported) {
        GetLowHigh(vector, out var low, out var high);
        return FromLowHigh(Sse41.RoundToNearestInteger(low), Sse41.RoundToNearestInteger(high));
      }

      return SoftwareFallback(vector);

      static Vector256<double> SoftwareFallback(Vector256<double> vector) {
        // TODO is this semantically equivalent to 'roundps'?
        return Vector256.Create(
            System.Math.Round(vector[0]),
            System.Math.Round(vector[1]),
            System.Math.Round(vector[2]),
            System.Math.Round(vector[3])
        );
      }
    }

    public static Vector256<double> Truncate(this Vector256<double> vector) {
      if (Avx.IsSupported) {
        return Avx.RoundToZero(vector);
      }

      if (Sse41.IsSupported) {
        Vector128Ext.GetLowHigh(vector, out var low, out var high);
        return FromLowHigh(Sse41.RoundToZero(low), Sse41.RoundToZero(high));
      }

      return SoftwareFallback(vector);

      static Vector256<double> SoftwareFallback(in Vector256<double> vector) {
        return Vector256.Create(
            System.Math.Truncate(vector[0]),
            System.Math.Truncate(vector[1]),
            System.Math.Truncate(vector[2]),
            System.Math.Truncate(vector[3])
        );
      }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<T> FromLowHigh<T>(in Vector128<T> low, in Vector128<T> high) where T : struct {
      return low.ToVector256Unsafe().WithUpper(high);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void GetLowHigh<T>(Vector256<T> vector, out Vector128<T> low, out Vector128<T> high) where T : struct {
      low = vector.GetLower();
      high = vector.GetUpper();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Sign(Vector256<double> vector) {
      var v = Vector256.Min(vector, Double.ONE);
      return Vector256.Max(v, Double.NEGATIVE_ONE);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<long> Sign(Vector256<long> vector) {
      var v = Vector256.Min(vector, Long.ONE);
      return Vector256.Max(v, Long.NEGATIVE_ONE);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> ExtractSign(Vector256<double> vector) {
      return vector & Double.MASK_NOT_SIGN;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<long> ExtractSign(Vector256<long> vector) {
      return vector & Long.MASK_NOT_SIGN;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Pow(in Vector256<double> x, in Vector256<double> y) {
      return Exp(y * Log(x));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> RSqrt(in Vector256<double> v) {
      return Double.ONE / Vector256.Sqrt(v);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> FillWithX(Vector256<double> vector) => Shuffle(vector, (byte)Shuffle4.xxxx);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> FillWithY(Vector256<double> vector) => Shuffle(vector, (byte)Shuffle4.yyyy);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> FillWithZ(Vector256<double> vector) => Shuffle(vector, (byte)Shuffle4.zzzz);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> FillWithW(Vector256<double> vector) => Shuffle(vector, (byte)Shuffle4.wwww);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> FastMultiplyAdd(Vector256<double> x, Vector256<double> y, Vector256<double> z) {
      if (Fma.IsSupported) {
        // FMA is faster than Add-Mul where it compiles to the native instruction, but it is not exactly semantically equivalent
        return Fma.MultiplyAdd(x, y, z);
      }

      return x * y + z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Vector256<double> Select(in Vector256<double> selector, in Vector256<double> trueVal, in Vector256<double> falseVal) {
      if (Avx.IsSupported) {
        return Avx.BlendVariable(falseVal, trueVal, selector);
      }

      return (trueVal & selector) | Vector256.AndNot(falseVal, selector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void ConvertLongToDouble(in Vector256<long> x, ref Vector256<double> y) {
      y = Avx.Subtract(Vector256.AsDouble(Avx2.Add(x, Long.MAGIC_LONG_ADD)), Double.MAGIC_DOUBLE_ADD);
    }

    internal static class Long {
      public static readonly Vector256<long> ONE_THOUSAND_TWENTY_THREE = Vector256.Create(0x3ffL);
      public static readonly Vector256<long> DECIMAL_MASK_FOR_DOUBLE = Vector256.Create(0xfffffffffffffL);
      public static readonly Vector256<long> EXPONENT_MASK_FOR_DOUBLE = Vector256.Create(1023L << 52);
      public static readonly Vector256<long> MAGIC_LONG_ADD = Vector256.AsInt64(Double.MAGIC_DOUBLE_ADD);

      public static readonly Vector256<long> MASK_SIGN = Vector256.Create(long.MaxValue);
      public static readonly Vector256<long> MASK_NOT_SIGN = Vector256.Create(~long.MaxValue);

      public static readonly Vector256<long> ONE = Vector256.Create(1L);
      public static readonly Vector256<long> NEGATIVE_ONE = Vector256.Create(-1L);
    }

    internal static class Double {
      public static readonly Vector256<double> MASK_SIGN = Vector256.Create(long.MaxValue).AsDouble();
      public static readonly Vector256<double> MASK_NOT_SIGN = Vector256.Create(~long.MaxValue).AsDouble();
      public static readonly Vector256<double> MAGIC_DOUBLE_ADD = Vector256.Create(6755399441055744.0);

      public static readonly Vector256<double> ONE = Vector256.Create(1.0);
      public static readonly Vector256<double> NEGATIVE_ONE = Vector256.Create(-1.0);
    }
  }
}
