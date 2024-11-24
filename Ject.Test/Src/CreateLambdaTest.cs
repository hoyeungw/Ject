using System;
using NUnit.Framework;
using Spare;

namespace Ject.Test {
  [TestFixture]
  public class CreateLambdaTest {
    [Test]
    public void AlphaTest() {
      var func = new Func<int, int, int>((a, b) => a + b).Method;
      func.Deco().Says("func");
    }
  }
}