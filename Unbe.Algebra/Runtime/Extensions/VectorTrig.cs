using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Unbe.Algebra {
  public static partial class Vector128Ext {
    public static readonly Vector128<float> ONE_DIV_PI = Vector128.Create(1f / Math.PI);
    public static readonly Vector128<float> ONE_DIV_PI2 = Vector128.Create(1f / Math.PI2);
    public static readonly Vector128<float> PI2 = Vector128.Create(Math.PI2);
    public static readonly Vector128<float> PI = Vector128.Create(Math.PI);
    public static readonly Vector128<float> PI_HALF = Vector128.Create(Math.PI_HALF);

    private static readonly Vector128<float> SinCoefficient0 = Vector128.Create(-0.16666667f, +0.0083333310f, -0.00019840874f, +2.7525562e-06f);
    private static readonly Vector128<float> SinCoefficient1 = Vector128.Create(-2.3889859e-08f, -0.16665852f, +0.0083139502f, -0.00018524670f);
    private const float SinCoefficient1Scalar = -2.3889859e-08f;
    private static readonly Vector128<float> SinCoefficient1Broadcast = Vector128.Create(SinCoefficient1Scalar);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Sin(in Vector128<float> vector) {
      if (Sse.IsSupported) {
        var vec = Mod2PI(vector);

        var abs = Vector128.Abs(vec); // Gets the absolute of vector

        var neg = (PI | ExtractSign(vec)) - vec;

        var comp = Sse.CompareLessThanOrEqual(abs, PI_HALF);
        vec = Sse41.BlendVariable(neg, vec, comp);

        var vectorSquared = vec * vec;

        // Polynomial approx
        var sc0 = SinCoefficient0;

        var constants = SinCoefficient1Broadcast;
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


    private static readonly Vector128<float> CosCoefficient0 = Vector128.Create(-0.5f, +0.041666638f, -0.0013888378f, +2.4760495e-05f);
    private static readonly Vector128<float> CosCoefficient1 = Vector128.Create(-2.6051615e-07f, -0.49992746f, +0.041493919f, -0.0012712436f);
    private const float CosCoefficient1Scalar = -2.6051615e-07f;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Cos(Vector128<float> vector) {
      if (Sse.IsSupported) {
        var vec = Mod2PI(vector);

        var abs = Vector128.Abs(vec); // Gets the absolute of vector

        var neg = (PI | ExtractSign(vec)) - vec;

        var comp = Sse.CompareLessThanOrEqual(abs, PI_HALF);
        vec = Sse41.BlendVariable(neg, vec, comp);

        var vectorSquared = vec * vec;

        vec = Sse41.BlendVariable(Float.NEGATIVE_ONE, Float.ONE, comp);

        // Polynomial approx
        var cc0 = CosCoefficient0;

        var constants = Vector128.Create(CosCoefficient1Scalar);
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

        var comp = Avx.Compare(abs, PI_HALF, FloatComparisonMode.OrderedLessThanOrEqualSignaling); ;

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
    public static Vector256<double> Mod2PI(in Vector256<double> vector) {
      return vector - PI2 * Round(vector * ONE_DIV_PI2);
    }
  }
}
