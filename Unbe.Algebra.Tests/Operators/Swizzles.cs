namespace Unbe.Algebra.Tests.Operators {
  internal class Swizzles {
    [Test]
    public void TestYZXW() {
      var source = new Float4(5, 1, 3, 0);
      var result = new Float4();
      result.yzxw = source;
      var expected = new Float4(3, 5, 1, 0);
      Assert.That(result, Is.EqualTo(expected));
    }
  }
}
