using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Analys;
using Ject.Test.Static;
using NUnit.Framework;
using Palett;
using Spare;
using Veho;
using Veho.Sequence;
using Veho.Types;

namespace Ject.Test.Strategies {
  public static class NaiveMethods {
    public static bool TryFind(this Type algebra, string name, out MethodInfo method) {
      return (method = algebra.StaticMethods().Find(it => it.Name == name)) != null;
    }

    public static bool TryFind(this Type algebra, Regex regex, out MethodInfo method) {
      return (method = algebra.StaticMethods().Find(it => regex.IsMatch(it.Name))) != null;
    }
  }

  public static class CachedMethods {
    public static Type Algebra;
    public static MethodInfo[] Methods;
    public static bool TryFind(this Type algebra, string name, out MethodInfo method) {
      if (Algebra != algebra || Methods == null) { Methods = algebra.StaticMethods(); }
      return (method = Methods.Find(it => it.Name == name)) != null;
    }
  }

  public static class LookupMethod {
    public static Type Algebra;
    public static ILookup<string, MethodInfo> Lookup;
    public static bool TryFind(this Type algebra, string name, out MethodInfo method) {
      if (Algebra != algebra || Lookup == null) { Lookup = algebra.GetMethods().ToLookup(m => m.Name); }
      method = null;
      return Lookup.Contains(name);
    }
  }


  [TestFixture]
  public class FindMethodStrategies {
    [Test]
    public void FindMethod() {
      var pointType = typeof(Point<double>);
      var regex = new Regex(@"^(?:op_)pow", RegexOptions.Compiled | RegexOptions.IgnoreCase);
      var (elapsed, result) = Valjoux.Strategies.Run(
        (int)5E+5,
        Seq.From<(string, Func<string, bool>)>(
          ("NaiveMethod", text => NaiveMethods.TryFind(typeof(Point<double>), text, out var some)),
          ("RegexMethod", text => NaiveMethods.TryFind(typeof(Point<double>), regex, out var some)),
          ("CachedMethod", text => CachedMethods.TryFind(pointType, text, out var some)),
          ("LookupMethod", text => LookupMethod.TryFind(typeof(Point<double>), text, out var some))
        ),
        Seq.From(
          ("op_Addition", "op_Addition"),
          ("op_Subtraction", "op_Subtraction"),
          ("op_Power", "op_Power")
        )
      );
      elapsed.Deco(orient: Operated.Rowwise, presets: (Presets.Subtle, Presets.Fresh)).Says("Elapsed");
      result.Deco(orient: Operated.Rowwise, presets: (Presets.Subtle, Presets.Fresh)).Says("Result");
    }
  }
}