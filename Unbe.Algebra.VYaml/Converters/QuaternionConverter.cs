using System;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization;
using static Unbe.Algebra.VYaml.Utils;

namespace Unbe.Algebra.VYaml {
  internal class QuaternionConverter : IYamlConverter, IYamlFormatter<Quaternion> {
    public void Register() {
      GeneratedResolver.Register(this);
    }

    public void Serialize(ref Utf8YamlEmitter emitter, Quaternion value, YamlSerializationContext context) {
      var v = value.value;
      emitter.BeginMapping();
      emitter.WriteString("x", ScalarStyle.Plain);
      context.Serialize(ref emitter, v.x);
      emitter.WriteString("y", ScalarStyle.Plain);
      context.Serialize(ref emitter, v.y);
      emitter.WriteString("z", ScalarStyle.Plain);
      context.Serialize(ref emitter, v.z);
      emitter.WriteString("w", ScalarStyle.Plain);
      context.Serialize(ref emitter, v.w);
      emitter.EndMapping();
    }

    public Quaternion Deserialize(ref YamlParser parser, YamlDeserializationContext context) {
      if (parser.IsNullScalar()) {
        parser.Read();
        return Quaternion.Identity;
      }

      float x = 0, y = 0, z = 0, w = 0;
      parser.ReadWithVerify(ParseEventType.MappingStart);
      while (!parser.End && parser.CurrentEventType != ParseEventType.MappingEnd) {
        if (parser.CurrentEventType != ParseEventType.Scalar || !parser.TryGetScalarAsSpan(out var key)) {
          throw new YamlSerializerException(parser.CurrentMark, "Custom type deserialization supports only string key");
        }

        if (key.SequenceEqual(XKeyUtf8Bytes)) {
          parser.Read(); // skip key
          x = parser.ReadScalarAsFloat();
        } else if (key.SequenceEqual(YKeyUtf8Bytes)) {
          parser.Read(); // skip key
          y = parser.ReadScalarAsFloat();
        } else if (key.SequenceEqual(ZKeyUtf8Bytes)) {
          parser.Read(); // skip key
          z = parser.ReadScalarAsFloat();
        } else if (key.SequenceEqual(WKeyUtf8Bytes)) {
          parser.Read(); // skip key
          w = parser.ReadScalarAsFloat();
        } else {
          parser.Read(); // skip key
          parser.SkipCurrentNode(); // skip value
        }
      }
      parser.ReadWithVerify(ParseEventType.MappingEnd);

      return new Quaternion(new Float4(x, y, z, w));
    } 
  }
}
