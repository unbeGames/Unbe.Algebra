namespace Unbe.Math {
  [AttributeUsage(AttributeTargets.Struct)]
  internal class MathTypeAttribute : Attribute {
    public Type type;
    public int dimensions;

    public MathTypeAttribute(Type type, int dimensions) {
      this.type = type;
      this.dimensions = dimensions;
    }
  }
}
