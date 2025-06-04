using static Unbe.Algebra.Noise.Utils;

namespace Unbe.Algebra.Noise {
  // 3D OpenSimplex2 case uses two offset rotated cube grids.

  /*
   * --- Rotation moved to TransformNoiseCoordinate method ---
   * const FNfloat R3 = (FNfloat)(2.0 / 3.0);
   * FNfloat r = (x + y + z) * R3; // Rotation, not skew
   * x = r - x; y = r - y; z = r - z;
  */
  public readonly struct OpenSimplex2 : INoise3D {
		public Real GetValue(int seed, Real3 p) {
      var ijk = FastRound(p);
      
      var p0 = p - ijk;
   
      var nSign = (Int3)(-1.0f - p0) | 1;

      var a0 = nSign * -p0;     

      ijk *= Prime;

      var value = 0f;
      var a = (0.6f - p0.x * p0.x) - (p0.y * p0.y + p0.z * p0.z);

      for (int l = 0; ; l++) {
        if (a > 0) {
          value += (a * a) * (a * a) * GradCoord(seed, ijk.x, ijk.y, ijk.z, p0.x, p0.y, p0.z);
        }

        if (a0.x >= a0.y && a0.x >= a0.z) {
          var b = a + a0.x + a0.x;
          if (b > 1) {
            b -= 1;
            value += (b * b) * (b * b) * GradCoord(seed, ijk.x - nSign.x * PrimeX, ijk.y, ijk.z, p0.x + nSign.x, p0.y, p0.z);
          }
        } else if (a0.y > a0.x && a0.y >= a0.z) {
          var b = a + a0.y + a0.y;
          if (b > 1) {
            b -= 1;
            value += (b * b) * (b * b) * GradCoord(seed, ijk.x, ijk.y - nSign.y * PrimeY, ijk.z, p0.x, p0.y + nSign.y, p0.z);
          }
        } else {
          var b = a + a0.z + a0.z;
          if (b > 1) {
            b -= 1;
            value += (b * b) * (b * b) * GradCoord(seed, ijk.x, ijk.y, ijk.z - nSign.z * PrimeZ, p0.x, p0.y, p0.z + nSign.z);
          }
        }

        if (l == 1) break;

        a0 = 0.5f - a0;
        p0 = nSign * a0;        

        a += (0.75f - a0.x) - (a0.y + a0.z);      

        ijk += (nSign >> 1) & Prime;

        nSign = -nSign;
        seed = ~seed;
      }

      return value * 32.69428253173828125f;      
    }
	}
}
