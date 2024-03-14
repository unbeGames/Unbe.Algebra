using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Unbe.Algebra {
  public static partial class Vector64Ext {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<float> Sin(in Vector64<float> vector) {
      return Vector64.Create(
        MathF.Sin(vector[0]),
        MathF.Sin(vector[1])
      );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<float> Cos(in Vector64<float> vector) {
      return Vector64.Create(
        MathF.Cos(vector[0]),
        MathF.Cos(vector[1])
      );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<float> Asin(in Vector64<float> vector) {
      return Vector64.Create(
        MathF.Asin(vector[0]),
        MathF.Asin(vector[1])
      );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SinCos(in Vector64<float> vector, out Vector64<float> sin, out Vector64<float> cos) {
      sin = Sin(vector);
      cos = Cos(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector64<float> Tan(in Vector64<float> vector) {
      return Vector64.Create(
        MathF.Tan(vector[0]),
        MathF.Tan(vector[1])
      );
    }
  }

  public static partial class Vector128Ext {
    public static readonly Vector128<float> ONE_DIV_PI = Vector128.Create(1f / Math.PI);
    public static readonly Vector128<float> ONE_DIV_PI2 = Vector128.Create(1f / Math.PI2);
    public static readonly Vector128<float> PI2 = Vector128.Create(Math.PI2);
    public static readonly Vector128<float> PI = Vector128.Create(Math.PI);
    public static readonly Vector128<float> PI_HALF = Vector128.Create(Math.PI_HALF);

    private static readonly Vector128<float> SinCoefficient0 = Vector128.Create(-0.16666667f, +0.0083333310f, -0.00019840874f, +2.7525562e-06f);
    private static readonly Vector128<float> SinCoefficient1 = Vector128.Create(-2.3889859e-08f, -0.16665852f, +0.0083139502f, -0.00018524670f);
 
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Sin(in Vector128<float> vector) {
      if (Sse.IsSupported) {
        var vec = Mod2PI(vector);

        var abs = Vector128.Abs(vec); // Gets the absolute of vector

        var neg = (PI | ExtractSign(vec)) - vec;

        var comp = Sse.CompareLessThanOrEqual(abs, PI_HALF);
        vec = Select(comp, vec, neg);

        var vectorSquared = vec * vec;

        // Polynomial approx
        var sc0 = SinCoefficient0;

        var constants = FillWithX(SinCoefficient1);
        var result = FastMultiplyAdd(constants, vectorSquared, FillWithW(sc0));

        constants = FillWithZ(sc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        constants = FillWithY(sc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        constants = FillWithX(sc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        result = FastMultiplyAdd(result, vectorSquared, Float.ONE);

        result *= vec;

        return result;
      }

      return SoftwareFallback(vector);

      static Vector128<float> SoftwareFallback(Vector128<float> vector) {
        return Vector128.Create(
            MathF.Sin(vector[0]),
            MathF.Sin(vector[1]),
            MathF.Sin(vector[2]),
            MathF.Sin(vector[3])
        );
      }
    }


    private static readonly Vector128<float> CosCoefficients0 = Vector128.Create(-0.5f, +0.041666638f, -0.0013888378f, +2.4760495e-05f);
    private static readonly Vector128<float> CosCoefficients1 = Vector128.Create(-2.6051615e-07f, -0.49992746f, +0.041493919f, -0.0012712436f);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Cos(Vector128<float> vector) {
      if (Sse.IsSupported) {
        var vec = Mod2PI(vector);

        var abs = Vector128.Abs(vec); // Gets the absolute of vector

        var neg = (PI | ExtractSign(vec)) - vec;

        var comp = Sse.CompareLessThanOrEqual(abs, PI_HALF);
        vec = Select(comp, vec, neg);

        var vectorSquared = vec * vec;

        vec = Select(comp, Float.ONE, Float.NEGATIVE_ONE);

        // Polynomial approx
        var cc0 = CosCoefficients0;

        var constants = FillWithX(CosCoefficients1);
        var result = FastMultiplyAdd(constants, vectorSquared, FillWithW(cc0));

        constants = FillWithZ(cc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        constants = FillWithY(cc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        constants = FillWithX(cc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        result = FastMultiplyAdd(result, vectorSquared, Float.ONE);

        result *= vec;

        return result;
      }

      return SoftwareFallback(vector);

      static Vector128<float> SoftwareFallback(Vector128<float> vector) {
        return Vector128.Create(
            MathF.Cos(vector[0]),
            MathF.Cos(vector[1]),
            MathF.Cos(vector[2]),
            MathF.Cos(vector[3])
        );
      }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SinCos(Vector128<float> vector, out Vector128<float> sin, out Vector128<float> cos) {
      if (Sse.IsSupported) {
        var vec = Mod2PI(vector);
        
        var abs = Vector128.Abs(vec); // Gets the absolute of vector

        var neg = (PI | ExtractSign(vec)) - vec;

        var comp = Sse.CompareLessThanOrEqual(abs, PI_HALF);

        vec = Select(comp, vec, neg);

        var vectorSquared = vec * vec;

        var cosVec = Select(comp, Float.ONE, Float.NEGATIVE_ONE);

        // Polynomial approx
        var sc0 = SinCoefficient0;

        var constants = FillWithX(SinCoefficient1);
        var result = FastMultiplyAdd(constants, vectorSquared, FillWithW(sc0));

        constants = FillWithZ(sc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        constants = FillWithY(sc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        constants = FillWithX(sc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        result = FastMultiplyAdd(result, vectorSquared, Float.ONE);

        result *= vec;

        sin = result;

        // Polynomial approx
        var cc0 = CosCoefficients0;

        constants = FillWithX(CosCoefficients1);
        result = FastMultiplyAdd(constants, vectorSquared, FillWithW(cc0));

        constants = FillWithZ(cc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        constants = FillWithY(cc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        constants = FillWithX(cc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        result = FastMultiplyAdd(result, vectorSquared, Float.ONE);

        result *= cosVec;

        cos = result;

        return;
      }

      SoftwareFallback(vector, out sin, out cos);

      static void SoftwareFallback(Vector128<float> vector, out Vector128<float> sin, out Vector128<float> cos) {
        sin = Sin(vector);
        cos = Cos(vector);
      }
    }

    private static readonly Vector128<float> TanCoefficients0 = Vector128.Create(1.0f, -4.667168334e-1f, 2.566383229e-2f, -3.118153191e-4f);
    private static readonly Vector128<float> TanCoefficients1 = Vector128.Create(4.981943399e-7f, -1.333835001e-1f, 3.424887824e-3f, -1.786170734e-5f);
    private static readonly Vector128<float> TanConstants = Vector128.Create(1.570796371f, 6.077100628e-11f, 0.000244140625f, 0.63661977228f);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Tan(Vector128<float> vector) {
      if (Sse.IsSupported) {
        var twoDivPi = FillWithW(TanConstants);

        var tc0 = FillWithX(TanConstants);
        var tc1 = FillWithY(TanConstants);
        var epsilon = FillWithZ(TanConstants);

        var va = vector * twoDivPi;
        va = Round(va);

        var vc = FastNegateMultiplyAdd(va, tc0, vector);

        var vb = Vector128.Abs(va);

        vc = FastNegateMultiplyAdd(va, tc1, vc);

        vb = ConvertToInt32(vb).AsSingle();

        var vc2 = vc * vc;

        var t7 = FillWithW(TanCoefficients1);
        var t6 = FillWithZ(TanCoefficients1);
        var t4 = FillWithX(TanCoefficients1);
        var t3 = FillWithW(TanCoefficients0);
        var t5 = FillWithY(TanCoefficients1);
        var t2 = FillWithZ(TanCoefficients0);
        var t1 = FillWithY(TanCoefficients0);
        var t0 = FillWithX(TanCoefficients0);

        var vbIsEven = (vb & Float.EPSILON).AsInt32();
        vbIsEven = CompareEqual(vbIsEven, Vector128<int>.Zero);

        var n = FastMultiplyAdd(vc2, t7, t6);
        var d = FastMultiplyAdd(vc2, t4, t3);
        n = FastMultiplyAdd(vc2, n, t5);
        d = FastMultiplyAdd(vc2, d, t2);
        n = vc2 * n;
        d = FastMultiplyAdd(vc2, d, t1);
        n = FastMultiplyAdd(vc, n, vc);

        var nearZero = InBounds(vc, epsilon);

        d = FastMultiplyAdd(vc2, d, t0);

        n = Select(nearZero, vc, n);
        d = Select(nearZero, Float.ONE, d);

        var r0 = -n;
        var r1 = n / d;
        r0 = d / r0;

        var isZero = Sse.CompareEqual(vector, Vector128<float>.Zero);

        var result = Select(vbIsEven.AsSingle(), r1, r0);

        result = Select(isZero, Vector128<float>.Zero, result);

        return result;
      }

      return SoftwareFallback(vector);

      static Vector128<float> SoftwareFallback(Vector128<float> vector) {
        return Vector128.Create(
            MathF.Tan(vector[0]),
            MathF.Tan(vector[1]),
            MathF.Tan(vector[2]),
            MathF.Tan(vector[3])
        );
      }
    }

    private static readonly Vector128<float> ArcCoefficients0 = Vector128.Create(+1.5707963050f, -0.2145988016f, +0.0889789874f, -0.0501743046f);
    private static readonly Vector128<float> ArcCoefficients1 = Vector128.Create(+0.0308918810f, -0.0170881256f, +0.0066700901f, -0.0012624911f);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Asin(Vector128<float> vector) {
      if (Sse.IsSupported) {
        var nonnegative = Sse.CompareGreaterThanOrEqual(vector, Float.ZERO);

        var x = Vector128.Abs(vector);

        // Compute (1-|V|), clamp to zero to avoid sqrt of negative number.
        var oneMValue = Float.ONE - x;
        var clampOneMValue = Vector128.Max(Float.ZERO, oneMValue);
        var root = Vector128.Sqrt(clampOneMValue);  // sqrt(1-|V|)

        // Compute polynomial approximation
        var AC1 = ArcCoefficients1;
        var vConstantsB = FillWithW(AC1);
        var vConstants = FillWithZ(AC1);
        var t0 = FastMultiplyAdd(vConstantsB, x, vConstants);

        vConstants = FillWithY(AC1);
        t0 = FastMultiplyAdd(t0, x, vConstants);

        vConstants = FillWithX(AC1);
        t0 = FastMultiplyAdd(t0, x, vConstants);

        var AC0 = ArcCoefficients0;
        vConstants = FillWithW(AC0);
        t0 = FastMultiplyAdd(t0, x, vConstants);

        vConstants = FillWithZ(AC0);
        t0 = FastMultiplyAdd(t0, x, vConstants);

        vConstants = FillWithY(AC0);
        t0 = FastMultiplyAdd(t0, x, vConstants);

        vConstants = FillWithX(AC0);
        t0 = FastMultiplyAdd(t0, x, vConstants);
        t0 *= root;

        var t1 = PI - t0;
        t0 = nonnegative + t0;
        t1 = Sse.AndNot(nonnegative, t1);
        t0 |= t1;
        t0 = PI_HALF - t0;
        return t0;
      }

      return SoftwareFallback(vector);

      static Vector128<float> SoftwareFallback(Vector128<float> vector) {
        return Vector128.Create(
            MathF.Asin(vector[0]),
            MathF.Asin(vector[1]),
            MathF.Asin(vector[2]),
            MathF.Asin(vector[3])
        );
      }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Mod2PI(in Vector128<float> vector) {
      return vector - PI2 * Round(vector * ONE_DIV_PI2);
    }
  }

  public static partial class Vector256Ext {
    public static readonly Vector256<double> ONE_DIV_PI = Vector256.Create(1 / Math.PI_DBL);
    public static readonly Vector256<double> ONE_DIV_PI2 = Vector256.Create(1f / Math.PI2_DBL);
    public static readonly Vector256<double> PI2 = Vector256.Create(Math.PI2_DBL);
    public static readonly Vector256<double> PI = Vector256.Create(Math.PI_DBL);
    public static readonly Vector256<double> PI_HALF = Vector256.Create(Math.PI_HALF_DBL);

    private static readonly Vector256<double> SinCoefficient0D = Vector256.Create(-0.16666667d, +0.0083333310d, -0.00019840874d, +2.7525562e-06d);
    private static readonly Vector256<double> SinCoefficient1D = Vector256.Create(-2.3889859e-08d, -0.16665852d, +0.0083139502d, -0.00018524670d);
    private const double SinCoefficient1DScalar = -2.3889859e-08d;

    /// <summary>
    /// Returns the sine of the specified angle.
    /// </summary>
    /// <param name="vector">The angle.</param>
    /// <returns>The sine of the given angle.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Sin(Vector256<double> vector) {
      if (Avx.IsSupported) {
        var vec = Mod2PI(vector);

        var abs = Vector256.Abs(vec); // Gets the absolute of vector

        var neg = (PI | ExtractSign(vec)) - vec;

        var comp = Avx.Compare(abs, PI_HALF, FloatComparisonMode.OrderedLessThanOrEqualSignaling);
        vec = Avx.BlendVariable(neg, vec, comp);

        var vectorSquared = vec * vec;

        // Polynomial approx
        var sc0 = SinCoefficient0D;

        var constants = Vector256.Create(SinCoefficient1DScalar);
        var result = FastMultiplyAdd(constants, vectorSquared, FillWithW(sc0));

        constants = FillWithZ(sc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        constants = FillWithY(sc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        constants = FillWithX(sc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        result = FastMultiplyAdd(result, vectorSquared, Double.ONE);

        result *= vec;

        return result;
      }

      return SoftwareFallback(vector);

      static Vector256<double> SoftwareFallback(Vector256<double> vector) {
        return Vector256.Create(
            System.Math.Sin(vector[0]),
            System.Math.Sin(vector[1]),
            System.Math.Sin(vector[2]),
            System.Math.Sin(vector[3])
        );
      }
    }

    private static readonly Vector256<double> CosCoefficient0D = Vector256.Create(-0.5d, +0.041666638d, -0.0013888378d, +2.4760495e-05d);
    private static readonly Vector256<double> CosCoefficient1D = Vector256.Create(-2.6051615e-07d, -0.49992746d, +0.041493919d, -0.0012712436d);
    private const double CosCoefficient1DScalar = -2.6051615e-07d;

    /// <summary>
    /// Returns the cosine of the specified angle.
    /// </summary>
    /// <param name="vector">The angle.</param>
    /// <returns>The cosine of the given angle.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Cos(Vector256<double> vector) {
      if (Avx.IsSupported) {
        var vec = Mod2PI(vector);

        var abs = Vector256.Abs(vec); // Gets the absolute of vector

        var neg = (PI | ExtractSign(vec)) - vec;

        var comp = Avx.Compare(abs, PI_HALF, FloatComparisonMode.OrderedLessThanOrEqualSignaling);

        vec = Avx.BlendVariable(neg, vec, comp);

        var vectorSquared = vec * vec;

        vec = Avx.BlendVariable(Double.NEGATIVE_ONE, Double.ONE, comp);

        // Polynomial approx
        var cc0 = CosCoefficient0D;

        var constants = Vector256.Create(CosCoefficient1DScalar);
        var result = FastMultiplyAdd(constants, vectorSquared, FillWithW(cc0));

        constants = FillWithZ(cc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        constants = FillWithY(cc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        constants = FillWithX(cc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        result = FastMultiplyAdd(result, vectorSquared, Double.ONE);

        result *= vec;

        return result;
      }

      return SoftwareFallback(vector);

      static Vector256<double> SoftwareFallback(Vector256<double> vector) {
        return Vector256.Create(
            System.Math.Cos(vector[0]),
            System.Math.Cos(vector[1]),
            System.Math.Cos(vector[2]),
            System.Math.Cos(vector[3])
        );
      }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SinCos(Vector256<double> vector, out Vector256<double> sin, out Vector256<double> cos) {
      if (Avx.IsSupported) {
        var vec = Mod2PI(vector);
      
        var abs = Vector256.Abs(vec); // Gets the absolute of vector

        var neg = (PI | ExtractSign(vec)) - vec;

        var comp = Avx.Compare(abs, PI_HALF, FloatComparisonMode.OrderedLessThanOrEqualSignaling);

        vec = Avx.BlendVariable(neg, vec, comp);

        var vectorSquared = vec * vec;

        var cosVec = Avx.BlendVariable(Double.NEGATIVE_ONE, Double.ONE, comp);

        // Polynomial approx
        Vector256<double> sc0 = SinCoefficient0D;

        Vector256<double> constants = Vector256.Create(SinCoefficient1DScalar);
        Vector256<double> result = FastMultiplyAdd(constants, vectorSquared, FillWithW(sc0));

        constants = FillWithZ(sc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        constants = FillWithY(sc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        constants = FillWithX(sc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        result = FastMultiplyAdd(result, vectorSquared, Double.ONE);

        result *= vec;

        sin = result;

        // Polynomial approx
        var cc0 = CosCoefficient0D;

        constants = Vector256.Create(CosCoefficient1DScalar);
        result = FastMultiplyAdd(constants, vectorSquared, FillWithW(cc0));

        constants = FillWithZ(cc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        constants = FillWithY(cc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        constants = FillWithX(cc0);
        result = FastMultiplyAdd(result, vectorSquared, constants);

        result = FastMultiplyAdd(result, vectorSquared, Double.ONE);

        result *= cosVec;

        cos = result;

        return;
      }

      SoftwareFallback(vector, out sin, out cos);

      static void SoftwareFallback(Vector256<double> vector, out Vector256<double> sin, out Vector256<double> cos) {
        sin = Sin(vector);
        cos = Cos(vector);
      }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<double> Mod2PI(in Vector256<double> vector) {
      return vector - PI2 * Round(vector * ONE_DIV_PI2);
    }
  }
}
