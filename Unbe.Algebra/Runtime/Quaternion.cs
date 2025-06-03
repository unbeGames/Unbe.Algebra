#pragma warning disable IDE1006

using System.Runtime.CompilerServices;
using static Unbe.Algebra.Maths;

namespace Unbe.Algebra {
  public partial struct Quaternion {
    public Float4 value;

    /// <summary>A Quaternion representing the identity transform.</summary>
    public static readonly Quaternion Identity = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);

    /// <summary>x component of the Quaternion.</summary>
    public float x { readonly get { return value[0]; } set { this.value[0] = value; } }
    /// <summary>y component of the Quaternion.</summary>
    public float y { readonly get { return value[1]; } set { this.value[1] = value; } }
    /// <summary>z component of the Quaternion.</summary>
    public float z { readonly get { return value[2]; } set { this.value[2] = value; } }
    /// <summary>w component of the Quaternion.</summary>
    public float w { readonly get { return value[3]; } set { this.value[3] = value; } }

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

    /// <summary>Constructs a unit quaternion from a Float3x3 rotation matrix. The matrix must be orthonormal.</summary>
    /// <param name="m">The Float3x3 orthonormal rotation matrix.</param>
    public Quaternion(Float3x3 m) {
      var u = m.c0;
      var v = m.c1;
      var w = m.c2;

      uint u_sign = (asuint(u.x) & 0x80000000);
      float t = v.y + asfloat(asuint(w.z) ^ u_sign);
      UInt4 u_mask = uint4((int)u_sign >> 31);
      UInt4 t_mask = uint4(asint(t) >> 31);

      float tr = 1.0f + abs(u.x);

      UInt4 sign_flips = uint4(0x00000000, 0x80000000, 0x80000000, 0x80000000) ^ (u_mask & uint4(0x00000000, 0x80000000, 0x00000000, 0x80000000)) ^ (t_mask & uint4(0x80000000, 0x80000000, 0x80000000, 0x00000000));

      value = float4(tr, u.y, w.x, v.z) + asfloat(asuint(float4(t, v.x, u.z, w.y)) ^ sign_flips);   // +---, +++-, ++-+, +-++

      value = asfloat((asuint(value) & ~u_mask) | (asuint(value.zwxy) & u_mask));
      value = asfloat((asuint(value.wzyx) & ~t_mask) | (asuint(value) & t_mask));
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

    /// <summary>
    /// Returns a quaternion view rotation given a unit length forward vector and a unit length up vector.
    /// The two input vectors are assumed to be unit length and not collinear.
    /// If these assumptions are not met use float3x3.LookRotationSafe instead.
    /// </summary>
    /// <param name="forward">The view forward direction.</param>
    /// <param name="up">The view up direction.</param>
    /// <returns>The quaternion view rotation.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion LookRotation(Float3 forward, Float3 up) {
      var t = normalize(cross(up, forward));
      return quaternion(new Float3x3(t, cross(forward, t), forward));
    }


    #region Euler Angles

    /// <summary>
    /// Returns a quaternion constructed by first performing a rotation around the x-axis, then the y-axis and finally the z-axis.
    /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
    /// </summary>
    /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
    /// <returns>The quaternion representing the Euler angle rotation in x-y-z order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion EulerXYZ(Float3 xyz) {
      sincos(0.5f * xyz, out var s, out var c);
      // s.x * c.y * c.z - s.y * s.z * c.x,
      // s.y * c.x * c.z + s.x * s.z * c.y,
      // s.z * c.x * c.y - s.x * s.y * c.z,
      // c.x * c.y * c.z + s.y * s.z * s.x
      return quaternion(float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * float4(c.xyz, s.x) * float4(-1.0f, 1.0f, -1.0f, 1.0f));
    }

    /// <summary>
    /// Returns a quaternion constructed by first performing a rotation around the x-axis, then the z-axis and finally the y-axis.
    /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
    /// </summary>
    /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
    /// <returns>The quaternion representing the Euler angle rotation in x-z-y order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion EulerXZY(Float3 xyz) {
      sincos(0.5f * xyz, out var s, out var c);
      // s.x * c.y * c.z + s.y * s.z * c.x,
      // s.y * c.x * c.z + s.x * s.z * c.y,
      // s.z * c.x * c.y - s.x * s.y * c.z,
      // c.x * c.y * c.z - s.y * s.z * s.x
      return quaternion(float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * float4(c.xyz, s.x) * float4(1.0f, 1.0f, -1.0f, -1.0f));
    }

    /// <summary>
    /// Returns a quaternion constructed by first performing a rotation around the y-axis, then the x-axis and finally the z-axis.
    /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
    /// </summary>
    /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
    /// <returns>The quaternion representing the Euler angle rotation in y-x-z order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion EulerYXZ(Float3 xyz) {
      sincos(0.5f * xyz, out var s, out var c);
      // s.x * c.y * c.z - s.y * s.z * c.x,
      // s.y * c.x * c.z + s.x * s.z * c.y,
      // s.z * c.x * c.y + s.x * s.y * c.z,
      // c.x * c.y * c.z - s.y * s.z * s.x
      return quaternion(float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * float4(c.xyz, s.x) * float4(-1.0f, 1.0f, 1.0f, -1.0f));
    }

    /// <summary>
    /// Returns a quaternion constructed by first performing a rotation around the y-axis, then the z-axis and finally the x-axis.
    /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
    /// </summary>
    /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
    /// <returns>The quaternion representing the Euler angle rotation in y-z-x order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion EulerYZX(Float3 xyz) {
      sincos(0.5f * xyz, out var s, out var c);
      // s.x * c.y * c.z - s.y * s.z * c.x,
      // s.y * c.x * c.z - s.x * s.z * c.y,
      // s.z * c.x * c.y + s.x * s.y * c.z,
      // c.x * c.y * c.z + s.y * s.z * s.x
      return quaternion(float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * float4(c.xyz, s.x) * float4(-1.0f, -1.0f, 1.0f, 1.0f));
    }

    /// <summary>
    /// Returns a quaternion constructed by first performing a rotation around the z-axis, then the x-axis and finally the y-axis.
    /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
    /// </summary>
    /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
    /// <returns>The quaternion representing the Euler angle rotation in z-x-y order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion EulerZXY(Float3 xyz) {
      sincos(0.5f * xyz, out var s, out var c);
      // s.x * c.y * c.z + s.y * s.z * c.x,
      // s.y * c.x * c.z - s.x * s.z * c.y,
      // s.z * c.x * c.y - s.x * s.y * c.z,
      // c.x * c.y * c.z + s.y * s.z * s.x
      return quaternion(float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * float4(c.xyz, s.x) * float4(1.0f, -1.0f, -1.0f, 1.0f));
    }

    /// <summary>
    /// Returns a quaternion constructed by first performing a rotation around the z-axis, then the y-axis and finally the x-axis.
    /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
    /// </summary>
    /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
    /// <returns>The quaternion representing the Euler angle rotation in z-y-x order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion EulerZYX(Float3 xyz) {
      sincos(0.5f * xyz, out var s, out var c);
      // s.x * c.y * c.z + s.y * s.z * c.x,
      // s.y * c.x * c.z - s.x * s.z * c.y,
      // s.z * c.x * c.y + s.x * s.y * c.z,
      // c.x * c.y * c.z - s.y * s.x * s.z
      return quaternion(float4(s.xyz, c.x) * c.yxxy * c.zzyz + s.yxxy * s.zzyz * float4(c.xyz, s.x) * float4(1.0f, -1.0f, 1.0f, -1.0f));
    }

    /// <summary>
    /// Returns a quaternion constructed by first performing a rotation around the x-axis, then the y-axis and finally the z-axis.
    /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
    /// </summary>
    /// <param name="x">The rotation angle around the x-axis in radians.</param>
    /// <param name="y">The rotation angle around the y-axis in radians.</param>
    /// <param name="z">The rotation angle around the z-axis in radians.</param>
    /// <returns>The quaternion representing the Euler angle rotation in x-y-z order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion EulerXYZ(float x, float y, float z) { return EulerXYZ(float3(x, y, z)); }

    /// <summary>
    /// Returns a quaternion constructed by first performing a rotation around the x-axis, then the z-axis and finally the y-axis.
    /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
    /// </summary>
    /// <param name="x">The rotation angle around the x-axis in radians.</param>
    /// <param name="y">The rotation angle around the y-axis in radians.</param>
    /// <param name="z">The rotation angle around the z-axis in radians.</param>
    /// <returns>The quaternion representing the Euler angle rotation in x-z-y order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion EulerXZY(float x, float y, float z) { return EulerXZY(float3(x, y, z)); }

    /// <summary>
    /// Returns a quaternion constructed by first performing a rotation around the y-axis, then the x-axis and finally the z-axis.
    /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
    /// </summary>
    /// <param name="x">The rotation angle around the x-axis in radians.</param>
    /// <param name="y">The rotation angle around the y-axis in radians.</param>
    /// <param name="z">The rotation angle around the z-axis in radians.</param>
    /// <returns>The quaternion representing the Euler angle rotation in y-x-z order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion EulerYXZ(float x, float y, float z) { return EulerYXZ(float3(x, y, z)); }

    /// <summary>
    /// Returns a quaternion constructed by first performing a rotation around the y-axis, then the z-axis and finally the x-axis.
    /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
    /// </summary>
    /// <param name="x">The rotation angle around the x-axis in radians.</param>
    /// <param name="y">The rotation angle around the y-axis in radians.</param>
    /// <param name="z">The rotation angle around the z-axis in radians.</param>
    /// <returns>The quaternion representing the Euler angle rotation in y-z-x order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion EulerYZX(float x, float y, float z) { return EulerYZX(float3(x, y, z)); }

    /// <summary>
    /// Returns a quaternion constructed by first performing a rotation around the z-axis, then the x-axis and finally the y-axis.
    /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
    /// </summary>
    /// <param name="x">The rotation angle around the x-axis in radians.</param>
    /// <param name="y">The rotation angle around the y-axis in radians.</param>
    /// <param name="z">The rotation angle around the z-axis in radians.</param>
    /// <returns>The quaternion representing the Euler angle rotation in z-x-y order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion EulerZXY(float x, float y, float z) { return EulerZXY(float3(x, y, z)); }

    /// <summary>
    /// Returns a quaternion constructed by first performing a rotation around the z-axis, then the y-axis and finally the x-axis.
    /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
    /// </summary>
    /// <param name="x">The rotation angle around the x-axis in radians.</param>
    /// <param name="y">The rotation angle around the y-axis in radians.</param>
    /// <param name="z">The rotation angle around the z-axis in radians.</param>
    /// <returns>The quaternion representing the Euler angle rotation in z-y-x order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion EulerZYX(float x, float y, float z) { return EulerZYX(float3(x, y, z)); }

    /// <summary>
    /// Returns a quaternion constructed by first performing 3 rotations around the principal axes in a given order.
    /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
    /// When the rotation order is known at compile time, it is recommended for performance reasons to use specific
    /// Euler rotation constructors such as EulerZXY(...).
    /// </summary>
    /// <param name="xyz">A float3 vector containing the rotation angles around the x-, y- and z-axis measures in radians.</param>
    /// <param name="order">The order in which the rotations are applied.</param>
    /// <returns>The quaternion representing the Euler angle rotation in the specified order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion Euler(Float3 xyz, RotationOrder order = RotationOrder.zxy) {
      switch (order) {
        case RotationOrder.xyz:
          return EulerXYZ(xyz);
        case RotationOrder.xzy:
          return EulerXZY(xyz);
        case RotationOrder.yxz:
          return EulerYXZ(xyz);
        case RotationOrder.yzx:
          return EulerYZX(xyz);
        case RotationOrder.zxy:
          return EulerZXY(xyz);
        case RotationOrder.zyx:
          return EulerZYX(xyz);
        default:
          return Quaternion.Identity;
      }
    }

    /// <summary>
    /// Returns a quaternion constructed by first performing 3 rotations around the principal axes in a given order.
    /// All rotation angles are in radians and clockwise when looking along the rotation axis towards the origin.
    /// When the rotation order is known at compile time, it is recommended for performance reasons to use specific
    /// Euler rotation constructors such as EulerZXY(...).
    /// </summary>
    /// <param name="x">The rotation angle around the x-axis in radians.</param>
    /// <param name="y">The rotation angle around the y-axis in radians.</param>
    /// <param name="z">The rotation angle around the z-axis in radians.</param>
    /// <param name="order">The order in which the rotations are applied.</param>
    /// <returns>The quaternion representing the Euler angle rotation in the specified order.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion Euler(float x, float y, float z, RotationOrder order = RotationOrder.standard) {
      return Euler(float3(x, y, z), order);
    }

    #endregion


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


  public static partial class Maths {
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

    /// <summary>Returns a unit quaternion constructed from a Float3x3 rotation matrix. The matrix must be orthonormal.</summary>
    /// <param name="m">The Float3x3 rotation matrix.</param>
    /// <returns>The quaternion constructed from a float3x3 matrix.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion quaternion(Float3x3 m) { return new Quaternion(m); }

    /// <summary>Returns a unit quaternion constructed from a Float4x4 matrix. The matrix must be orthonormal.</summary>
    /// <param name="m">The Float4x4 matrix (must be orthonormal).</param>
    /// <returns>The Quaternion constructed from a float4x4 matrix.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion quaternion(Float4x4 m) { return new Quaternion(m); }

    /// <summary>Returns the result of transforming the Quaternion b by the Quaternion a.</summary>
    /// <param name="a">The quaternion on the left.</param>
    /// <param name="b">The quaternion on the right.</param>
    /// <returns>The result of transforming Quaternion b by the Quaternion a.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Quaternion mul(Quaternion a, Quaternion b) {
      return quaternion(a.value.wwww * b.value + (a.value.xyzx * b.value.wwwx + a.value.yzxy * b.value.zxyy) * float4(1.0f, 1.0f, 1.0f, -1.0f) - a.value.zxyz * b.value.yzxz);
    }

    /// <summary>Returns the result of transforming a vector by a quaternion.</summary>
    /// <param name="q">The quaternion transformation.</param>
    /// <param name="v">The vector to transform.</param>
    /// <returns>The transformation of vector v by quaternion q.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float3 mul(Quaternion q, Float3 v) {
      var t = 2 * cross(q.value.xyz, v);
      return v + q.value.w * t + cross(q.value.xyz, t);
    }       
  }
}
