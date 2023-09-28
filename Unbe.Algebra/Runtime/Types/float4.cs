#pragma warning disable 0660, 0661, 8981, IDE1006

using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using half = System.Half;

namespace Unbe.Algebra {
  [MathType(type: typeof(float), dimensions: 4)]
  public partial struct float4 : IEquatable<float4>, IFormattable { 
    public Vector128<float> value;

    /// <summary>Constructs a float4 vector from a single half value by converting it to float and assigning it to every component.</summary>
    /// <param name="v">half to convert to float4</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float4(half v) {
      value = Vector128.Create((float)v);
    }

    /// <summary>Implicitly converts a single half value to a float4 vector by converting it to float and assigning it to every component.</summary>
    /// <param name="v">half to convert to float4</param>
    /// <returns>Converted value.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator float4(half v) { return new float4(v); }
  }
}
