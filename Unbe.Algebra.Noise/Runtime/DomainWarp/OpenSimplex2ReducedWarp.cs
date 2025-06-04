using static Unbe.Algebra.Noise.Utils;

namespace Unbe.Algebra.Noise {
	public struct OpenSimplex2ReducedWarp : IDomainWarp3D {
		public void Warp(int seed, float frequency, float amp, Real3 ps, ref Real3 p) {
      ps *= frequency;
      amp *= 7.71604938271605f;

      var ijk = FastRound(ps);

      Real3 p0 = ps - ijk;
      var nSign = (Int3)(-p0 - 1) | 1;

      Real3 a0 = nSign * -p0;

      ijk *= Prime;      

      Real3 pv = 0;
      
      Real a = (0.6f - p0.x * p0.x) - (p0.y * p0.y + p0.z * p0.z);
      for (int l = 0; ; l++) {
        if (a > 0) {
          Real aaaa = (a * a) * (a * a);          
          GradCoordOut(seed, ijk.x, ijk.y, ijk.z, out var po);
          pv += aaaa * po;          
        }

        var b = a;
        var ijk1 = ijk;
        var p1 = p0;
        
        if (a0.x >= a0.y && a0.x >= a0.z) {
          p1.x += nSign.x;
          b = b + a0.x + a0.x;
          ijk1.x -= nSign.x * PrimeX;
        } else if (a0.y > a0.x && a0.y >= a0.z) {
          p1.y += nSign.y;
          b = b + a0.y + a0.y;
          ijk1.y -= nSign.y * PrimeY;
        } else {
          p1.z += nSign.z;
          b = b + a0.z + a0.z;
          ijk1.z -= nSign.z * PrimeZ;
        }

        if (b > 1) {
          b -= 1;
          Real bbbb = (b * b) * (b * b);
          GradCoordOut(seed, ijk1.x, ijk1.y, ijk1.z, out var po);

          pv += bbbb * po;
        }

        if (l == 1) break;

        a0 = 0.5f - a0;
        
        p0 = nSign * a0;
        
        a += (0.75f - a0.x) - (a0.y + a0.z);

        ijk += (nSign >> 1) & Prime; 

        nSign = -nSign;
        seed += 1293373;
      }

      p += pv * amp;
    }
	}
}
