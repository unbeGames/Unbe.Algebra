namespace Unbe.Algebra.Noise {
	public interface IDomainWarp3D {
		void Warp(int seed, float frequency, float amp, Real3 origPoint, ref Real3 point);
	}
}