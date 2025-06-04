namespace Unbe.Algebra.Noise {
	public interface INoise3D {
		Real GetValue(int seed, Real3 point);
	}

	public interface INoiseDeriv3D {
		Real GetValue(int seed, Real3 point, out Real3 deriv);
	}
}
