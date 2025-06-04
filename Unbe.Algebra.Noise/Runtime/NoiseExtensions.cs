namespace Unbe.Algebra.Noise {
	public static class NoiseExtensions {
		public static Real GetValue<T>(this T noise, int seed, Real x, Real y, Real z) where T : struct, INoise3D {
			return noise.GetValue(seed, new Real3(x, y, z));
		}

		public static Real GetValue<T>(this T noise, int seed, float frequency, Real3 point) where T : struct, INoise3D {
			point *= frequency;
			return noise.GetValue(seed, point);
		}

		public static Real GetValue<T>(this T noise, int seed, float frequency, Real x, Real y, Real z) where T : struct, INoise3D {
			return noise.GetValue(seed, frequency, new Real3(x, y, z));
		}
	}
}
