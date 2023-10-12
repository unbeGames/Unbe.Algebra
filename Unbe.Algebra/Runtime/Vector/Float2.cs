#pragma warning disable 0660, 0661, 8981, IDE1006

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using half = System.Half;

namespace Unbe.Algebra {
  [MathType(type: typeof(float), dimensionX: 2)]
  public partial struct Float2 : IEquatable<Float2>, IFormattable { 
    public Vector64<float> value;

    /// <summary>Constructs a float4 vector from a single half value by converting it to float and assigning it to every component.</summary>
    /// <param name="v">half to convert to float4</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float2(half v) {
      value = Vector64.Create((float)v);
    }

    /// <summary>Implicitly converts a single half value to a float4 vector by converting it to float and assigning it to every component.</summary>
    /// <param name="v">half to convert to float4</param>
    /// <returns>Converted value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Float2(half v) { return new Float2(v); }
  }
}
