using System.Runtime.CompilerServices;
using static Unbe.Algebra.Maths;

namespace Unbe.Algebra {
  public partial struct Float3x3 {
    /// <summary>
    /// Constructs a Float3x3 from the upper left 3x3 of a Float4x4.
    /// </summary>
    /// <param name="f4x4"><see cref="Float4x4"/> to extract a float3x3 from.</param>
    public Float3x3(Float4x4 f4x4) {
      c0 = f4x4.c0.xyz;
      c1 = f4x4.c1.xyz;
      c2 = f4x4.c2.xyz;
    }

    /// <summary>Constructs a Float3x3 matrix from a unit quaternion.</summary>
    /// <param name="q">The quaternion rotation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float3x3(Quaternion q) {
      var v = q.value;
      var v2 = v + v;

      var npn = uint3(0x80000000, 0x00000000, 0x80000000);
      var nnp = uint3(0x80000000, 0x80000000, 0x00000000);
      var pnn = uint3(0x00000000, 0x80000000, 0x80000000);

      c0 = v2.y * asfloat(asuint(v.yxw) ^ npn) - v2.z * asfloat(asuint(v.zwx) ^ pnn) + float3(1, 0, 0);
      c1 = v2.z * asfloat(asuint(v.wzy) ^ nnp) - v2.x * asfloat(asuint(v.yxw) ^ npn) + float3(0, 1, 0);
      c2 = v2.x * asfloat(asuint(v.zwx) ^ pnn) - v2.y * asfloat(asuint(v.wzy) ^ nnp) + float3(0, 0, 1);
    } 
  }

  public partial struct Float4x4 {
    /// <summary>
    /// Returns a Float4x4 matrix representing a rotation around a unit axis by an angle in radians.
    /// The rotation direction is clockwise when looking along the rotation axis towards the origin i.e. left-handed coordinate system.
    /// </summary>
    /// <param name="axis">The axis of rotation.</param>
    /// <param name="angle">The angle of rotation in radians.</param>
    /// <returns>The Float4x4 matrix representing the rotation about an axis.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float4x4 AxisAngle(Float3 axis, float angle) {    
      sincos(angle, out var sina, out var cosa);

      var u = float4(axis, 0.0f);  
      var uInvCosa = u - u * cosa;  // u * (1.0f - cosa);
      var t = float4(u.xyz * sina, cosa);

      var ppnp = uint4(0x00000000, 0x00000000, 0x80000000, 0x00000000);
      var nppp = uint4(0x80000000, 0x00000000, 0x00000000, 0x00000000);
      var pnpp = uint4(0x00000000, 0x80000000, 0x00000000, 0x00000000);
      var mask = uint4(0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFF, 0x00000000);

      return new Float4x4(
        u.x * uInvCosa + asfloat((asuint(t.wzyx) ^ ppnp) & mask),
        u.y * uInvCosa + asfloat((asuint(t.zwxx) ^ nppp) & mask),
        u.z * uInvCosa + asfloat((asuint(t.yxwx) ^ pnpp) & mask),
        float4(0.0f, 0.0f, 0.0f, 1.0f)
      );
    }

    /// <summary>
    /// Returns a Float4x4 matrix representing a combined scale-, rotation- and translation transform.
    /// Equivalent to mul(translationTransform, mul(rotationTransform, scaleTransform)).
    /// </summary>
    /// <param name="translation">The translation vector.</param>
    /// <param name="rotation">The quaternion rotation.</param>
    /// <param name="scale">The scaling factors of each axis.</param>
    /// <returns>The Float4x4 matrix representing the translation, rotation, and scale by the inputs.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float4x4 TRS(Float3 translation, Quaternion rotation, Float3 scale) {
      var r = new Float3x3(rotation);
      return new Float4x4(float4(r.c0 * scale.x, 0f),
                          float4(r.c1 * scale.y, 0f),
                          float4(r.c2 * scale.z, 0f),
                          float4(translation, 1f));
    }

    /// <summary>Returns a Float4x4 scale matrix given 3 axis scales.</summary>
    /// <param name="s">The uniform scaling factor.</param>
    /// <returns>The Float4x4 matrix that represents a uniform scale.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float4x4 Scale(float s) {
      return new Float4x4( s, 0f, 0f, 0f,
                          0f,  s, 0f, 0f,
                          0f, 0f,  s, 0f,
                          0f, 0f, 0f, 1f);
    }

    /// <summary>Returns a Float4x4 scale matrix given a Float3 vector containing the 3 axis scales.</summary>
    /// <param name="x">The x-axis scaling factor.</param>
    /// <param name="y">The y-axis scaling factor.</param>
    /// <param name="z">The z-axis scaling factor.</param>
    /// <returns>The Float4x4 matrix that represents a non-uniform scale.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float4x4 Scale(float x, float y, float z) {
      return new Float4x4( x, 0f, 0f, 0f,
                          0f,  y, 0f, 0f,
                          0f, 0f,  z, 0f,
                          0f, 0f, 0f, 1f);
    }

    /// <summary>Returns a Float4x4 scale matrix given a Float3 vector containing the 3 axis scales.</summary>
    /// <param name="scales">The vector containing scale factors for each axis.</param>
    /// <returns>The Float4x4 matrix that represents a non-uniform scale.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float4x4 Scale(Float3 scales) {
      return Scale(scales.x, scales.y, scales.z);
    }

    /// <summary>Returns a Float4x4 translation matrix given a Float3 translation vector.</summary>
    /// <param name="vector">The translation vector.</param>
    /// <returns>The Float4x4 translation matrix.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float4x4 Translate(Float3 vector) {
      return new Float4x4(float4(1f, 0f, 0f, 0f),
                          float4(0f, 1f, 0f, 0f),
                          float4(0f, 0f, 1f, 0f),
                          float4(vector.xyz, 1f));
    }
  }
}
