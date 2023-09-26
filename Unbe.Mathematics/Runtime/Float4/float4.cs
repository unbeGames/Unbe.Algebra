using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

#pragma warning disable 0660, 0661, IDE1006

namespace Unbe.Mathematics2 {
  [MathType(type: typeof(float), dimensions: 4)]
  public partial struct float4 : IEquatable<float4>, IFormattable {
    public Vector128<float> value;

    /// <summary>x component of the vector.</summary>
    public float x { readonly get { return value.GetElement(0); } set { this.value = this.value.WithElement(0, value); } }
    /// <summary>y component of the vector.</summary>
    public float y { readonly get { return value.GetElement(1); } set { this.value = this.value.WithElement(1, value); } }
    /// <summary>z component of the vector.</summary>
    public float z { readonly get { return value.GetElement(2); } set { this.value = this.value.WithElement(2, value); } }
    /// <summary>w component of the vector.</summary>
    public float w { readonly get { return value.GetElement(3); } set { this.value = this.value.WithElement(3, value); } }


    /// <summary>Constructs a float4 vector from four float values.</summary>
    /// <param name="x">The constructed vector's x component will be set to this value.</param>
    /// <param name="y">The constructed vector's y component will be set to this value.</param>
    /// <param name="z">The constructed vector's z component will be set to this value.</param>
    /// <param name="w">The constructed vector's w component will be set to this value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float4(float x, float y, float z, float w) {
      value = Vector128.Create(x, y, z, w);
    }

    /// <summary>Constructs a float4 vector from a single float value by assigning it to every component.</summary>
    /// <param name="v">float to convert to float4</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float4(float v) {
      value = Vector128.Create(v);
    }

    /// <summary>Constructs a float4 vector from Vector128.</summary>
    /// <param name="v">Vector128 to convert to float4</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float4(Vector128<float> v) {
      value = v;
    }

    public bool Equals(float4 other) {
      return Vector128.EqualsAll(value, other.value);
    }

    public string ToString(string format, IFormatProvider formatProvider) {
      return string.Format("float4({0}f, {1}f, {2}f, {3}f)", x.ToString(format, formatProvider), y.ToString(format, formatProvider), z.ToString(format, formatProvider), w.ToString(format, formatProvider));
    }

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

    /// <summary>Returns a string representation of the float4.</summary>
    /// <returns>String representation of the value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override readonly string ToString() {
      return string.Format($"float4({x}f, {y}f, {z}f, {w}f)");
    }
  }
}
