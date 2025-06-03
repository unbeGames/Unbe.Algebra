# Unbe.Algebra

**Unbe.Algebra** is a vector and matrix library written in C# that uses hardware intrinsics and have a shader-like syntax, similar to SIMD or HLSL.

## Usage

```c#
using Unbe.Algebra;
using static Unbe.Algebra.Maths;

namespace MyNamespace {
  public class Example {
    public void Test() {
      var vec1 = float3(2, 5, 3);[THIRD-PARTY-NOTICES.txt](THIRD-PARTY-NOTICES.txt)
      var vec2 = float3(1, 2, 3);
      
      vec1 = normalizesafe(vec1);
      vec2 = normalize(vec2);
      
      var d = dot(vec1, vec2);
      
      var vec3 = float4(vec1.xy, vec2.z, 1);
      vec3.xyz *= d;

      var mat = Float4x4.Identity;
      mat.c3.xyz = float3(5);

      var result = mul(mat, vec3);
    }
  }
}
```
## Project status
I am using this library for my game engine development and introducing new types / math functions as I need them. Feel free to post your requests in the issues or open up pull requests if you need something.

## Why?
HLSL math library is a very well designed and have a great API which is very useful for math-heavy 3D applications like games. It is also very convenient to use a very similar syntax when porting algorithms from shader world and back.

### Again, why not to use built-in C# math library?
While C# have a very performant VectorX* classes that use SIMD, they have an object-oriented API and only support float type.

### Why not Unity.Mathematics then?
While it is great, but it was built for using with Burst only. The performance is not great when compiled using NET. It also have a restrictive license.

### When to use?
If you are brave enough to use a library in a very early stage of development and prefer HLSL-syntax for your math or want to use vectors and matrices not only of float type.

## License & Copyright
Unbe.Algebra is licensed under the MIT license. Full copyright belongs to @Arugin.
