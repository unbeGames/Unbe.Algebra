using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Unbe.Algebra {
  public static partial class Vector128Ext {

    private static readonly Vector128<float> SinCoefficient0 = Vector128.Create(-0.16666667f, +0.0083333310f, -0.00019840874f, +2.7525562e-06f);
    private static readonly Vector128<float> SinCoefficient1 = Vector128.Create(-2.3889859e-08f, -0.16665852f, +0.0083139502f, -0.00018524670f);
    private const float SinCoefficient1Scalar = -2.3889859e-08f;
    private static readonly Vector128<float> SinCoefficient1Broadcast = Vector128.Create(SinCoefficient1Scalar);

    public static readonly Vector128<float> ONE_DIV_PI = Vector128.Create(1 / Math.PI);
    public static readonly Vector128<float> ONE_DIV_PI2 = Vector128.Create(1 / Math.PI2);
    public static readonly Vector128<float> PI2 = Vector128.Create(Math.PI2);
    public static readonly Vector128<float> PI = Vector128.Create(Math.PI);
    public static readonly Vector128<float> PI_HALF = Vector128.Create(Math.PI_HALF);

    public static readonly Vector128<float> P3 = Vector128.Create(-0.166666666666663509013977f);
    public static readonly Vector128<float> P5 = Vector128.Create(0.008333333333299304989001f);
    public static readonly Vector128<float> P7 = Vector128.Create(-0.00019841269828860068271f);
    public static readonly Vector128<float> P9 = Vector128.Create(0.00000275573170815073144f);
    public static readonly Vector128<float> P11 = Vector128.Create(-0.00000002505191090496049f);
    public static readonly Vector128<float> P13 = Vector128.Create(0.000000000160490521296459f);
    public static readonly Vector128<float> P15 = Vector128.Create(-0.0000000000007384998082865f);

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
        var result = Fma.MultiplyAdd(constants, vectorSquared, FillWithW(sc0));

        constants = FillWithZ(sc0);
        result = Fma.MultiplyAdd(result, vectorSquared, constants);

        constants = FillWithY(sc0);
        result = Fma.MultiplyAdd(result, vectorSquared, constants);

        constants = FillWithX(sc0);
        result = Fma.MultiplyAdd(result, vectorSquared, constants);

        result = Fma.MultiplyAdd(result, vectorSquared, Float.ONE);

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> SinOtherImpl(in Vector128<float> x) {
      // Since sin() is periodic around 2pi, this converts x into the range of [0, 2pi]
      var xt = Sse.Subtract(x, Sse.Multiply(PI2, Sse41.Floor(Sse.Multiply(x, ONE_DIV_PI2))));

      // Since sin() in [0, 2pi] is an odd function around pi, this converts the range to [0, pi], then stores whether
      // or not the result needs to be negated in negend.
      var negend = Sse.CompareGreaterThan(xt, PI);
      xt = Sse.Subtract(xt, Sse.And(negend, PI));

      negend = Sse.And(negend, Float.NEGATIVE_TWO);
      negend = Sse.Add(negend, Float.ONE);

      var nanend = Sse.CompareNotEqual(x, x);
      nanend = Sse.Add(nanend, Sse.CompareGreaterThan(x, Float.HIGH));
      nanend = Sse.Add(nanend, Sse.CompareLessThan(x, Float.LOW));

      // Since sin() on [0, pi] is an even function around pi/2, this "folds" the range into [0, pi/2]. I.e. 3pi/5 becomes 2pi/5.
      xt = Sse.Subtract(PI_HALF, Vector128.Abs(Sse.Subtract(xt, PI_HALF)));

      var xsq = Sse.Multiply(xt, xt);

      // This is an odd-only Taylor series approximation of sin() on [0, pi/2]. 
      var yy = Fma.MultiplyAdd(P15, xsq, P13);
      yy = Fma.MultiplyAdd(yy, xsq, P11);
      yy = Fma.MultiplyAdd(yy, xsq, P9);
      yy = Fma.MultiplyAdd(yy, xsq, P7);
      yy = Fma.MultiplyAdd(yy, xsq, P5);
      yy = Fma.MultiplyAdd(yy, xsq, P3);
      yy = Fma.MultiplyAdd(yy, xsq, Float.ONE);
      yy = Sse.Multiply(yy, xt);

      return Fma.MultiplyAdd(yy, negend, nanend);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<float> Mod2PI(in Vector128<float> vector) {
      return vector - Round(vector * ONE_DIV_PI2) * PI2;
    }
  }
}
