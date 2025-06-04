using static Unbe.Algebra.Maths;
using static Unbe.Algebra.Noise.Utils;

namespace Unbe.Algebra.Noise {
	public struct BasicGridWarp : IDomainWarp3D {     
		public void Warp(int seed, float frequency, float amp, Real3 ps, ref Real3 p) {     
      Real3 pf = ps * frequency;      
      var p0 = FastFloor(pf);
      
      Real3 s = InterpHermite(pf - p0);
      
      p0 *= Prime;

      var p1 = p0 + Prime;

      int hash0 = Hash(seed, p0.x, p0.y, p0.z) & (255 << 2);
      int hash1 = Hash(seed, p1.x, p0.y, p0.z) & (255 << 2);

      Real lx0x = lerp(RandVecs3D[hash0], RandVecs3D[hash1], s.x);
      Real ly0x = lerp(RandVecs3D[hash0 | 1], RandVecs3D[hash1 | 1], s.x);
      Real lz0x = lerp(RandVecs3D[hash0 | 2], RandVecs3D[hash1 | 2], s.x);

      hash0 = Hash(seed, p0.x, p1.y, p0.z) & (255 << 2);
      hash1 = Hash(seed, p1.x, p1.y, p0.z) & (255 << 2);

      Real lx1x = lerp(RandVecs3D[hash0], RandVecs3D[hash1], s.x);
      Real ly1x = lerp(RandVecs3D[hash0 | 1], RandVecs3D[hash1 | 1], s.x);
      Real lz1x = lerp(RandVecs3D[hash0 | 2], RandVecs3D[hash1 | 2], s.x);

      Real lx0y = lerp(lx0x, lx1x, s.y);
      Real ly0y = lerp(ly0x, ly1x, s.y);
      Real lz0y = lerp(lz0x, lz1x, s.y);

      hash0 = Hash(seed, p0.x, p0.y, p1.z) & (255 << 2);
      hash1 = Hash(seed, p1.x, p0.y, p1.z) & (255 << 2);

      lx0x = lerp(RandVecs3D[hash0], RandVecs3D[hash1], s.x);
      ly0x = lerp(RandVecs3D[hash0 | 1], RandVecs3D[hash1 | 1], s.x);
      lz0x = lerp(RandVecs3D[hash0 | 2], RandVecs3D[hash1 | 2], s.x);

      hash0 = Hash(seed, p0.x, p1.y, p1.z) & (255 << 2);
      hash1 = Hash(seed, p1.x, p1.y, p1.z) & (255 << 2);

      lx1x = lerp(RandVecs3D[hash0], RandVecs3D[hash1], s.x);
      ly1x = lerp(RandVecs3D[hash0 | 1], RandVecs3D[hash1 | 1], s.x);
      lz1x = lerp(RandVecs3D[hash0 | 2], RandVecs3D[hash1 | 2], s.x);

      p.x += lerp(lx0y, lerp(lx0x, lx1x, s.y), s.z) * amp;
      p.y += lerp(ly0y, lerp(ly0x, ly1x, s.y), s.z) * amp;
      p.z += lerp(lz0y, lerp(lz0x, lz1x, s.y), s.z) * amp;
    }
	}
}
