using static Unbe.Algebra.Maths;
using static Unbe.Algebra.Noise.Utils;

namespace Unbe.Algebra.Noise {
	public readonly struct Value3D : INoise3D {
		public Real GetValue(int seed, Real3 p) {
      var p0 = FastFloor(p);

      var s = InterpHermite(p - p0);

      p0 *= Prime;

      var p1 = p0 + Prime;

      var xf00 = lerp(ValCoord(seed, p0.x, p0.y, p0.z), ValCoord(seed, p1.x, p0.y, p0.z), s.x);
      var xf10 = lerp(ValCoord(seed, p0.x, p1.y, p0.z), ValCoord(seed, p1.x, p1.y, p0.z), s.x);
      var xf01 = lerp(ValCoord(seed, p0.x, p0.y, p1.z), ValCoord(seed, p1.x, p0.y, p1.z), s.x);
      var xf11 = lerp(ValCoord(seed, p0.x, p1.y, p1.z), ValCoord(seed, p1.x, p1.y, p1.z), s.x);

      var yf0 = lerp(xf00, xf10, s.y);
      var yf1 = lerp(xf01, xf11, s.y);

      return lerp(yf0, yf1, s.z);
    }
	}
}
