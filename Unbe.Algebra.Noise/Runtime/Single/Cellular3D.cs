using static Unbe.Algebra.Maths;
using static Unbe.Algebra.Noise.Utils;
using System.Runtime.CompilerServices;

namespace Unbe.Algebra.Noise {
  public interface ICellularDistanceFunction {
    void CalcDistance(int seed, Real3 p, Int3 pr, Int3 primedBase, float cellularJitter, ref Real distance0, ref Real distance1, ref int closestHash);
  }


  public  struct Cellular3D<T> : INoise3D where T : struct, ICellularDistanceFunction {
    public float cellularJitterModifier;

    public T distanceFunc;

		public readonly Real GetValue(int seed, Real3 p) {
      var pr = FastRound(p);

      var distance0 = Real.MaxValue;
      var distance1 = Real.MaxValue;
      var closestHash = 0;

      var cellularJitter = 0.39614353f * cellularJitterModifier;

      var primedBase = (pr - 1) * Prime;

      distanceFunc.CalcDistance(seed, p, pr, primedBase, cellularJitter, ref distance0, ref distance1, ref closestHash);

      return distance0 - 1; // TODO more return types
    }
	}

	public readonly struct EuclideanCellularDistance : ICellularDistanceFunction {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CalcDistance(int seed, Real3 p, Int3 pr, Int3 primedBase, float cellularJitter, ref Real distance0, ref Real distance1, ref int closestHash) {
      for (int xi = pr.x - 1; xi <= pr.x + 1; xi++) {
        var yPrimed = primedBase.y;

        for (int yi = pr.y - 1; yi <= pr.y + 1; yi++) {
          var zPrimed = primedBase.z;

          for (int zi = pr.z - 1; zi <= pr.z + 1; zi++) {
            var hash = Hash(seed, primedBase.x, yPrimed, zPrimed);
            var idx = hash & (255 << 2);

            var vecX = (Real)(xi - p.x) + RandVecs3D[idx] * cellularJitter;
            var vecY = (Real)(yi - p.y) + RandVecs3D[idx | 1] * cellularJitter;
            var vecZ = (Real)(zi - p.z) + RandVecs3D[idx | 2] * cellularJitter;

            var newDistance = vecX * vecX + vecY * vecY + vecZ * vecZ;

            distance1 = max(min(distance1, newDistance), distance0);
            if (newDistance < distance0) {
              distance0 = newDistance;
              closestHash = hash;
            }
            zPrimed += PrimeZ;
          }
          yPrimed += PrimeY;
        }
        primedBase.x += PrimeX;
      }
      distance0 = sqrt(distance0);
    }
	}

	public readonly struct ManhattanCellularDistance : ICellularDistanceFunction {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CalcDistance(int seed, Real3 p, Int3 pr, Int3 primedBase, float cellularJitter, ref Real distance0, ref Real distance1, ref int closestHash) {
      for (int xi = pr.x - 1; xi <= pr.x + 1; xi++) {
        var yPrimed = primedBase.y;

        for (int yi = pr.y - 1; yi <= pr.y + 1; yi++) {
          var zPrimed = primedBase.z;

          for (int zi = pr.z - 1; zi <= pr.z + 1; zi++) {
            var hash = Hash(seed, primedBase.x, yPrimed, zPrimed);
            var idx = hash & (255 << 2);

            var vecX = (float)(xi - p.x) + RandVecs3D[idx] * cellularJitter;
            var vecY = (float)(yi - p.y) + RandVecs3D[idx | 1] * cellularJitter;
            var vecZ = (float)(zi - p.z) + RandVecs3D[idx | 2] * cellularJitter;

            var newDistance = abs(vecX) + abs(vecY) + abs(vecZ);

            distance1 = max(min(distance1, newDistance), distance0);
            if (newDistance < distance0) {
              distance0 = newDistance;
              closestHash = hash;
            }
            zPrimed += PrimeZ;
          }
          yPrimed += PrimeY;
        }
        primedBase.x += PrimeX;
      }
    }
	}

  public readonly struct HybridCellularDistance : ICellularDistanceFunction {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CalcDistance(int seed, Real3 p, Int3 pr, Int3 primedBase, float cellularJitter, ref Real distance0, ref Real distance1, ref int closestHash) {
      for (int xi = pr.x - 1; xi <= pr.x + 1; xi++) {
        var yPrimed = primedBase.y;

        for (int yi = pr.y - 1; yi <= pr.y + 1; yi++) {
          var zPrimed = primedBase.z;

          for (int zi = pr.z - 1; zi <= pr.z + 1; zi++) {
            var hash = Hash(seed, primedBase.x, yPrimed, zPrimed);
            var idx = hash & (255 << 2);

            var vecX = (float)(xi - p.x) + RandVecs3D[idx] * cellularJitter;
            var vecY = (float)(yi - p.y) + RandVecs3D[idx | 1] * cellularJitter;
            var vecZ = (float)(zi - p.z) + RandVecs3D[idx | 2] * cellularJitter;

            var newDistance = (abs(vecX) + abs(vecY) + abs(vecZ)) + (vecX * vecX + vecY * vecY + vecZ * vecZ);

            distance1 = max(min(distance1, newDistance), distance0);
            if (newDistance < distance0) {
              distance0 = newDistance;
              closestHash = hash;
            }
            zPrimed += PrimeZ;
          }
          yPrimed += PrimeY;
        }
        primedBase.x += PrimeX;
      }
    }
  }
}
