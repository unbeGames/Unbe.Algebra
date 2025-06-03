using System;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization;
using static Unbe.Algebra.VYaml.Utils;

namespace Unbe.Algebra.VYaml {
  internal class UInt4Converter : IYamlConverter, IYamlFormatter<UInt4> {
    public void Register() {
      GeneratedResolver.Register(this);
    }

    public void Serialize(ref Utf8YamlEmitter emitter, UInt4 value, YamlSerializationContext context) {
      emitter.BeginMapping();
      emitter.WriteString("x", ScalarStyle.Plain);
      context.Serialize(ref emitter, value.x);
      emitter.WriteString("y", ScalarStyle.Plain);
      context.Serialize(ref emitter, value.y);
      emitter.WriteString("z", ScalarStyle.Plain);
      context.Serialize(ref emitter, value.y);
      emitter.WriteString("w", ScalarStyle.Plain);
      context.Serialize(ref emitter, value.w);
      emitter.EndMapping();
    }

    public UInt4 Deserialize(ref YamlParser parser, YamlDeserializationContext context) {
      if (parser.IsNullScalar()) {
        parser.Read();
        return default;
      }

      uint x = 0, y = 0, z = 0, w = 0;
      parser.ReadWithVerify(ParseEventType.MappingStart);
      while (!parser.End && parser.CurrentEventType != ParseEventType.MappingEnd) {
        if (parser.CurrentEventType != ParseEventType.Scalar || !parser.TryGetScalarAsSpan(out var key)) {
          throw new YamlSerializerException(parser.CurrentMark, "Custom type deserialization supports only string key");
        }

        if (key.SequenceEqual(XKeyUtf8Bytes)) {
          parser.Read(); // skip key
          x = parser.ReadScalarAsUInt32();
        } else if (key.SequenceEqual(YKeyUtf8Bytes)) {
          parser.Read(); // skip key
          y = parser.ReadScalarAsUInt32();
        } else if (key.SequenceEqual(ZKeyUtf8Bytes)) {
          parser.Read(); // skip key
          z = parser.ReadScalarAsUInt32();
        } else if (key.SequenceEqual(WKeyUtf8Bytes)) {
          parser.Read(); // skip key
          w = parser.ReadScalarAsUInt32();
        } else {
          parser.Read(); // skip key
          parser.SkipCurrentNode(); // skip value
        }
      }
      parser.ReadWithVerify(ParseEventType.MappingEnd);

      return new UInt4(x, y, z, w);
    } 
  }
}
