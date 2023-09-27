using System.Runtime.CompilerServices;
using static Unbe.Math.math;

namespace Unbe.Math {
  public struct Random {
    /// <summary>
    /// The random number generator state. It should not be zero.
    /// </summary>
    public uint state;

    /// <summary>
    /// Constructs a Random instance with a given seed value. The seed must be non-zero.
    /// </summary>
    /// <param name="seed">The seed to initialize with.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Random(uint seed) {
      state = seed;
      CheckInitState();
      NextState();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private uint NextState() {
      CheckState();
      uint t = state;
      state ^= state << 13;
      state ^= state >> 17;
      state ^= state << 5;
      return t;
    }

    /// <summary>Returns a uniformly random float4 value with all components in the interval [0, 1).</summary>
    /// <returns>A uniformly random float4 value in the range [0, 1).</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float4 NextFloat4() {
      return asfloat(0x3f800000 | (new uint4(NextState(), NextState(), NextState(), NextState()) >> 9)) - 1.0f;
    }

    private readonly void CheckInitState() {
      if (state == 0)
        throw new System.ArgumentException("Seed must be non-zero");
    }

    private readonly void CheckState() {
      if (state == 0)
        throw new System.ArgumentException("Invalid state 0. Random object has not been properly initialized");
    }
  }
}
