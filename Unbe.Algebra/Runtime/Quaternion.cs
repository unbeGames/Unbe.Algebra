#pragma warning disable IDE1006

using System.Runtime.CompilerServices;
using static Unbe.Algebra.Math;

namespace Unbe.Algebra {
  public partial struct Quaternion {
    public Float4 value;

    /// <summary>A Quaternion representing the identity transform.</summary>
    public static readonly Quaternion Identity = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);

    /// <summary>Constructs a Quaternion from four float values.</summary>
    /// <param name="x">The quaternion x component.</param>
    /// <param name="y">The quaternion y component.</param>
    /// <param name="z">The quaternion z component.</param>
    /// <param name="w">The quaternion w component.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Quaternion(float x, float y, float z, float w) { value.x = x; value.y = y; value.z = z; value.w = w; }

    /// <summary>Constructs a Quaternion from Float4 vector.</summary>
    /// <param name="value">The Quaternion xyzw component values.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Quaternion(Float4 value) { this.value = value; }

    /// <summary>Constructs a Quaternion vector from System.Numerics.Quaternion.</summary>
    /// <param name="v">System.Numerics.Quaternion to convert to Quaternion</param>
    /// <returns>Constructed value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Quaternion(System.Numerics.Quaternion q) {
      value = new Float4(q.X, q.Y, q.Z, q.W);
    }

    /// <summary>Implicitly converts a Float4 vector to a quaternion.</summary>
    /// <param name="v">The quaternion xyzw component values.</param>
    /// <returns>The quaternion constructed from a Float4 vector.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Quaternion(Float4 v) { return new Quaternion(v); }

    /// <summary>Constructs a unit quaternion from an orthonormal Float4x4 matrix.</summary>
    /// <param name="m">The Float4x4 orthonormal rotation matrix.</param>
    public Quaternion(Float4x4 m) {
      var u = m.c0;
      var v = m.c1;
      var w = m.c2;

      uint uSign = (asuint(u.x) & 0x80000000);
      float t = v.y + asfloat(asuint(w.z) ^ uSign);
      UInt4 uMask = uint4((int)uSign >> 31);
      UInt4 tMask = uint4(asint(t) >> 31);

      float tr = 1.0f + abs(u.x);

      UInt4 signFlips = uint4(0x00000000, 0x80000000, 0x80000000, 0x80000000) ^ (uMask & uint4(0x00000000, 0x80000000, 0x00000000, 0x80000000)) ^ (tMask & uint4(0x80000000, 0x80000000, 0x80000000, 0x00000000));

      // +---, +++-, ++-+, +-++
      value = float4(tr, u.y, w.x, v.z) + asfloat(asuint(float4(t, v.x, u.z, w.y)) ^ signFlips);

      value = asfloat((asuint(value) & ~uMask) | (asuint(value.zwxy) & uMask));
      value = asfloat((asuint(value.wzyx) & ~tMask) | (asuint(value) & tMask));

      value = normalize(value);
    }


    /// <summary>
    /// Returns a quaternion representing a rotation around a unit axis by an angle in radians.
    /// The rotation direction is clockwise when looking along the rotation axis towards the origin.
    /// </summary>
    /// <param name="axis">The axis of rotation.</param>
    /// <param name="angle">The angle of rotation in radians.</param>
    /// <returns>The quaternion representing a rotation around an axis.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion AxisAngle(Float3 axis, float angle) {     
      sincos(0.5f * angle, out var sina, out var cosa);
      return quaternion(float4(axis * sina, cosa));
    }

    /// <summary>Constructs a Quaternion from a System.Numerics.Quaternion.</summary>
    /// <param name="v">System.Numerics.Quaternion to convert to Quaternion.</param>
    /// <returns>Converted value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Quaternion(System.Numerics.Quaternion q) { return new Quaternion(q); }

    /// <summary>Constructs a System.Numerics.Quaternion vector from a Quaternion.</summary>
    /// <param name="v">Quaternion to convert to System.Numerics.Quaternion.</param>
    /// <returns>Converted value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator System.Numerics.Quaternion(Quaternion q) { return new System.Numerics.Quaternion(q.value.x, q.value.y, q.value.z, q.value.w); }
  }


  public static partial class Math {
    /// <summary>Returns a quaternion constructed from four float values.</summary>
    /// <param name="x">The x component of the quaternion.</param>
    /// <param name="y">The y component of the quaternion.</param>
    /// <param name="z">The z component of the quaternion.</param>
    /// <param name="w">The w component of the quaternion.</param>
    /// <returns>The quaternion constructed from individual components.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion quaternion(float x, float y, float z, float w) { return new Quaternion(x, y, z, w); }

    /// <summary>Returns a quaternion constructed from a float4 vector.</summary>
    /// <param name="value">The float4 containing the components of the quaternion.</param>
    /// <returns>The quaternion constructed from a float4.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion quaternion(Float4 value) { return new Quaternion(value); }

    /// <summary>Returns a unit quaternion constructed from a float3x3 rotation matrix. The matrix must be orthonormal.</summary>
    /// <param name="m">The float3x3 rotation matrix.</param>
    /// <returns>The quaternion constructed from a float3x3 matrix.</returns>
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Quaternion quaternion(Float3x3 m) { return new Quaternion(m); }

    /// <summary>Returns a unit quaternion constructed from a float4x4 matrix. The matrix must be orthonormal.</summary>
    /// <param name="m">The float4x4 matrix (must be orthonormal).</param>
    /// <returns>The quaternion constructed from a float4x4 matrix.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion quaternion(Float4x4 m) { return new Quaternion(m); }


  }
}
