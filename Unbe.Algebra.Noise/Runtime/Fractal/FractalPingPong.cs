using static Unbe.Algebra.Maths;
using static Unbe.Algebra.Noise.Utils;

namespace Unbe.Algebra.Noise {
	public struct FractalPingPong<T> : INoise3D where T : struct, INoise3D {
    public readonly T mNoise;
    public readonly int octaves;
    public readonly float lacunarity;
    public readonly float gain;
    public readonly float weightedStrength;
    public readonly float pingPongStength;
    public readonly float fractalBounding;
    public Real3 offset;

    public FractalPingPong(int octaves, float lacunarity = 1.99f, float gain = 0.5f, float weightedStrength = 0, float pingPongStength = 2) : this(new T(), octaves, lacunarity, gain, weightedStrength, pingPongStength) {

    }

    public FractalPingPong(T noise, int octaves, float lacunarity = 1.99f, float gain = 0.5f, float weightedStrength = 0, float pingPongStength = 2) {
      mNoise = noise;
      this.octaves = octaves;
      this.lacunarity = lacunarity;
      this.gain = gain;
      this.pingPongStength = pingPongStength;
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
        Real noise = PingPong((mNoise.GetValue(seed++, point) + 1) * pingPongStength);
        sum += (noise - 0.5f) * 2 * amp;
        amp *= lerp(1.0f, noise, weightedStrength);

        point = point * lacunarity + offset;
        amp *= gain;
        offset *= lacunarity;
      }

      return sum;
    }
	}
}
