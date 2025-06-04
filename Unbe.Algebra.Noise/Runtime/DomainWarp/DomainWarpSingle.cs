using System.Runtime.CompilerServices;

namespace Unbe.Algebra.Noise {
	public struct DomainWarpSingle<T, U> : INoise3D where T : struct, IDomainWarp3D where U : struct, INoise3D {
		public readonly T warp;
		public readonly U noise;
		public readonly int warpSeed;
		public readonly float warpFrequency;
		public readonly float warpAmp;

		public DomainWarpSingle(int warpSeed, float warpFrequency, float warpAmp) : this(new T(), new U(), warpSeed, warpFrequency, warpAmp) {
		}

		public DomainWarpSingle(U noise, int warpSeed, float warpFrequency, float warpAmp) : this(new T(), noise, warpSeed, warpFrequency, warpAmp) {
		}

		public DomainWarpSingle(T warp, U noise, int warpSeed, float warpFrequency, float warpAmp) {
			this.warp = warp;
			this.noise = noise;
			this.warpSeed = warpSeed;
			this.warpFrequency = warpFrequency;
			this.warpAmp = warpAmp;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Real GetValue(int seed, Real3 point) {
			warp.Warp(warpSeed, warpFrequency, warpAmp, point, ref point);
			return noise.GetValue(seed, point);
		}
	}
}
