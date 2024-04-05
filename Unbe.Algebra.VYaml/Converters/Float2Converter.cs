using System;
using VYaml.Emitter;
using VYaml.Parser;
using VYaml.Serialization;
using static Unbe.Algebra.VYaml.Utils;

namespace Unbe.Algebra.VYaml {
  internal class Float2Converter : YamlConverter, IYamlFormatter<Float2> { 
    public void Register() {
      GeneratedResolver.Register(this);
    }

    public void Serialize(ref Utf8YamlEmitter emitter, Float2 value, YamlSerializationContext context) {
      emitter.BeginMapping();
      emitter.WriteString("x", ScalarStyle.Plain);
      context.Serialize(ref emitter, value.x);
      emitter.WriteString("y", ScalarStyle.Plain);
      context.Serialize(ref emitter, value.y);
      emitter.EndMapping();
    }

    public Float2 Deserialize(ref YamlParser parser, YamlDeserializationContext context) {
      if (parser.IsNullScalar()) {
        parser.Read();
        return default;
      }


      float x = 0, y = 0;
      parser.ReadWithVerify(ParseEventType.MappingStart);
      while (!parser.End && parser.CurrentEventType != ParseEventType.MappingEnd) {
        if (parser.CurrentEventType != ParseEventType.Scalar) {
          throw new YamlSerializerException(parser.CurrentMark, "Custom type deserialization supports only string key");
        }

        if (!parser.TryGetScalarAsSpan(out var key)) {
          throw new YamlSerializerException(parser.CurrentMark, "Custom type deserialization supports only string key");
        }

        if (key.SequenceEqual(xKeyUtf8Bytes)) {
          parser.Read(); // skip key
          x = context.DeserializeWithAlias<float>(ref parser);
        } else if (key.SequenceEqual(yKeyUtf8Bytes)) {
          parser.Read(); // skip key
          y = context.DeserializeWithAlias<float>(ref parser);
        } else {
          parser.Read(); // skip key
          parser.SkipCurrentNode(); // skip value
          continue;
        }
      }
      parser.ReadWithVerify(ParseEventType.MappingEnd);

      return new Float2(x, y);
    }
  }
}
