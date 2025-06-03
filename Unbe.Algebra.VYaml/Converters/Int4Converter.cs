using System;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization;
using static Unbe.Algebra.VYaml.Utils;

namespace Unbe.Algebra.VYaml {
  internal class Int4Converter : IYamlConverter, IYamlFormatter<Int4> {
    public void Register() {
      GeneratedResolver.Register(this);
    }

    public void Serialize(ref Utf8YamlEmitter emitter, Int4 value, YamlSerializationContext context) {
      emitter.BeginMapping();
      emitter.WriteString("x", ScalarStyle.Plain);
      context.Serialize(ref emitter, value.x);
      emitter.WriteString("y", ScalarStyle.Plain);
      context.Serialize(ref emitter, value.y);
      emitter.WriteString("z", ScalarStyle.Plain);
      context.Serialize(ref emitter, value.z);
      emitter.WriteString("w", ScalarStyle.Plain);
      context.Serialize(ref emitter, value.w);
      emitter.EndMapping();
    }

    public Int4 Deserialize(ref YamlParser parser, YamlDeserializationContext context) {
      if (parser.IsNullScalar()) {
        parser.Read();
        return default;
      }

      int x = 0, y = 0, z = 0, w = 0;
      parser.ReadWithVerify(ParseEventType.MappingStart);
      while (!parser.End && parser.CurrentEventType != ParseEventType.MappingEnd) {
        if (parser.CurrentEventType != ParseEventType.Scalar || !parser.TryGetScalarAsSpan(out var key)) {
          throw new YamlSerializerException(parser.CurrentMark, "Custom type deserialization supports only string key");
        }

        if (key.SequenceEqual(XKeyUtf8Bytes)) {
          parser.Read(); // skip key
          x = parser.ReadScalarAsInt32();
        } else if (key.SequenceEqual(YKeyUtf8Bytes)) {
          parser.Read(); // skip key
          y = parser.ReadScalarAsInt32();
        } else if (key.SequenceEqual(ZKeyUtf8Bytes)) {
          parser.Read(); // skip key
          z = parser.ReadScalarAsInt32();
        } else if (key.SequenceEqual(WKeyUtf8Bytes)) {
          parser.Read(); // skip key
          w = parser.ReadScalarAsInt32();
        } else {
          parser.Read(); // skip key
          parser.SkipCurrentNode(); // skip value
        }
      }
      parser.ReadWithVerify(ParseEventType.MappingEnd);

      return new Int4(x, y, z, w);
    } 
  }
}
