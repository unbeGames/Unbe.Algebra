using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

#pragma warning disable 0660, 0661, IDE1006

namespace Unbe.Mathematics2 {
  [MathType(type: typeof(float), dimensions: 4)]
  public partial struct float4 : IEquatable<float4>, IFormattable {
    public Vector128<float> value; 

    /// <summary>Returns the result of a componentwise subtraction operation on a float4 vector and a float value.</summary>
    /// <param name="lhs">Left hand side float4 to use to compute componentwise subtraction.</param>
    /// <param name="rhs">Right hand side float to use to compute componentwise subtraction.</param>
    /// <returns>float4 result of the componentwise subtraction.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float4 operator -(float4 lhs, float rhs) { return new float4(Vector128.Subtract(lhs.value, Vector128.Create(rhs))); }


    /// <summary>Returns the result of a componentwise multiplication operation on two float4 vectors.</summary>
    /// <param name="lhs">Left hand side float4 to use to compute componentwise multiplication.</param>
    /// <param name="rhs">Right hand side float4 to use to compute componentwise multiplication.</param>
    /// <returns>float4 result of the componentwise multiplication.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float4 operator *(float4 lhs, float4 rhs) { return new float4(Vector128.Multiply(lhs.value, rhs.value)); }
  }
}
