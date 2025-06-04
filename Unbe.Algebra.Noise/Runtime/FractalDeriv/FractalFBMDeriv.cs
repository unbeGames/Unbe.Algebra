using static Unbe.Algebra.Maths;
using static Unbe.Algebra.Noise.Utils;
using System.Runtime.CompilerServices;

namespace Unbe.Algebra.Noise {
	public struct FractalFBMDeriv<T> : INoise3D, INoiseDeriv3D where T : struct, INoiseDeriv3D {
    public readonly T mNoise;
    public readonly int octaves;
    public readonly float gain;
    public readonly float weightedStrength;
    public readonly float lacunarity;
    public readonly float fractalBounding;
    public Real3 offset;

    public FractalFBMDeriv(int octaves, float lacunarity = 1.99f, float gain = 0.5f, float weightedStrength = 0) : this(new T(), octaves, lacunarity, gain, weightedStrength) {

    }

    public FractalFBMDeriv(T noise, int octaves, float lacunarity = 1.99f, float gain = 0.5f, float weightedStrength = 0) {
      mNoise = noise;
      this.octaves = octaves;
      this.lacunarity = lacunarity;
      this.gain = gain;
      this.weightedStrength = weightedStrength;
      offset = Real3.Zero;
      fractalBounding = CalculateFractalBounding(octaves, gain);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Real GetValue(int mSeed, Real3 point) {
      return GetValue(mSeed, point, out _);
    }

		public readonly Real GetValue(int mSeed, Real3 point, out Real3 dsum) {
      int seed = mSeed;
      Real sum = 0;
      Real amp = fractalBounding;
      Real3 offset = this.offset;
      dsum = Real3.Zero;

      for (int i = 0; i < octaves; i++) {
        Real noise = mNoise.GetValue(seed++, point, out var deriv);
        dsum += deriv;
        sum += noise * amp / (1 + dot(dsum, dsum));
        amp *= lerp(1.0f, (noise + 1) * 0.5f, weightedStrength);

        point = point * lacunarity + offset;
        amp *= gain;
        offset *= lacunarity;
      }

      return sum;
    }
	}
}
