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

  }
 }
