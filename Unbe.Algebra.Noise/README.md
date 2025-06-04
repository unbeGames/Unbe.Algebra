# Unbe.Algebra.Noise

This is a port of [FastNoise Lite library](https://github.com/Auburn/FastNoise) using Unbe.Algebra for Net platform.

## Limitations

This library has several limitations right now, that can be addressed in the future:

* only 3d noise was ported;
* point transformations not yet implemented;
* Cellular3D only returns distance as a result;
* code optimisation is in progress;
* there are only fractal derivatives implemented right now.

### Examples

Simple noise:

```C#
using Unbeg.Algebra;
using Unbeg.Algebra.Noise;

namespace MyNamespace {
    ...
    int seed = 15;
    var point = new Float3(3,3,3);
    var noise = new Perlin3D();
    float result = noise.GetValue(seed, point);
    ...
}
```

Using fractals:

```C#
int octaves = 5;
float lacunarity = 1.99f;
float gain = 0.5f;
var noise = new FractalBillow<Value3D>(octaves, lacunarity, gain);
```

Nested fractals:

```C#
var fractal = new FractalRiged<ValueCubic3D>(octaves, lacunarity, gain);
var noise = new FractalFBM<FractalRiged<ValueCubic3D>>(fractal, octaves, lacunarity, gain);
```

Using warp:
```C#
var noise = new DomainWarpSingle<BasicGridWarp, ValueCubic3D>(warpSeed, warpFrequency, warpAmp);

```

## Double precision

Add `NOISE_DOUBLE_PRECISION` into scripting define symbols.
