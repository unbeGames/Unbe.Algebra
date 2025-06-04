using static Unbe.Algebra.Maths;
using static Unbe.Algebra.Noise.Utils;

namespace Unbe.Algebra.Noise {
	public struct FractalRiged<T> : INoise3D where T : struct, INoise3D {
    public readonly T mNoise;
    public readonly int octaves;
    public readonly float gain;
    public readonly float weightedStrength;
    public readonly float lacunarity;
    public readonly float fractalBounding;
    public Real3 offset;

    public FractalRiged(int octaves, float lacunarity = 1.99f, float gain = 0.5f, float weightedStrength = 0) : this(new T(), octaves, lacunarity, gain, weightedStrength) {

    }

    public FractalRiged(T noise, int octaves, float lacunarity = 1.99f, float gain = 0.5f, float weightedStrength = 0) {
      mNoise = noise;
      this.octaves = octaves;
      this.lacunarity = lacunarity;
      this.gain = gain;
      this.weightedStrength = weightedStrength;
      offset = Real3.Zero;
      fractalBounding = CalculateFractalBounding(octaves, gain);
    }

    public readonly Real GetValue(int mSeed, Real3 point) {
      int seed = mSeed;
      Real sum = 0;
      Real amp = fractalBounding;
      Real3 offset = this.offset;

      for (int i = 0; i < octaves; i++) {
        Real noise = abs(mNoise.GetValue(seed++, point));
        sum += (noise * -2 + 1) * amp;
        amp *= lerp(1.0f, 1 - noise, weightedStrength);

        point = point * lacunarity + offset;
        amp *= gain;
        offset *= lacunarity;
      }

      return sum;
    }
	}
}
