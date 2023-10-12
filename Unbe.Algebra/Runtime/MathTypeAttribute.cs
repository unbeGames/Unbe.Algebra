namespace Unbe.Algebra {
  [AttributeUsage(AttributeTargets.Struct)]
  internal class MathTypeAttribute : Attribute {
    public Type type;
    public int dimensionX;
    public int dimensionY;

    public MathTypeAttribute(Type type, int dimensionX, int dimensionY = 1) {
      this.type = type;
      this.dimensionX = dimensionX;
      this.dimensionY = dimensionY;
    }
  }
}
