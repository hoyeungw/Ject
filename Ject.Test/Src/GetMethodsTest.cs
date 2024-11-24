using System;
using System.Reflection;
using Ject.Test.Static;
using NUnit.Framework;
using Veho.Vector;

namespace Ject.Test {
  [TestFixture]
  public class GetMethodsTest {
    [Test]
    public void PointTest() {
      typeof(Point<double>).GetMethods().Map(x => x.Deco()).Iterate(x => {
        Console.WriteLine($">> {x}");
      });
    }
    [Test]
    public void DoubleTest() {
      typeof(double).GetMethods().Map(x => x.Deco()).Iterate(x => {
        Console.WriteLine($">> {x}");
      });
      var method = typeof(double).GetMethod("op_Addition");
      Console.WriteLine($">> {method?.Deco()}");
    }

    [Test]
    public void StringTest() {
      typeof(string).GetMethods().Map(x => x.Deco()).Iterate(x => {
        Console.WriteLine($">> {x}");
      });
    }
  }

  // public static class ReflectDeco {
  //   // public static MethodInfo[] methods(this Type T) {
  //   //     return T.GetMethods()
  //   //             // .SkipWhile(method => method.MethodImplementationFlags == MethodImplAttributes.InternalCall)
  //   //             .ToArray();
  //   //   }
  //   public static void SpectreMethod(this Type T) {
  //     $"Spectre Method: [{T}]".wL();
  //     T.GetMethods()
  //      .Iterate((idx, it) => $"[{idx}] {it.toBrief()}".wL());
  //   }
  //   public static void SpectreProperty(this Type T) {
  //     $"Spectre Property: [{T}]".wL();
  //     T.GetProperties()
  //      .Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal))
  //      .Iterate(it => it.Deco().wL());
  //   }
  //   public static void SpectreMethod<T>(T instance) {
  //     $"Spectre Property: [{typeof(T)}] ({instance})".wL();
  //     typeof(T).GetMethods()
  //              .Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal))
  //              .Iterate((idx, prt) => $"<{idx}> {prt.toBrief(instance)}".wL());
  //   }
  //   public static void SpectreProperty<T>(T instance) {
  //     $"Spectre Property: [{typeof(T)}] ({instance})".wL();
  //     typeof(T).GetProperties()
  //              .Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
  //       .Iterate((idx, prt) => $"<{idx}> {prt.Info(instance)}".wL());
  //   }
  // }

  public static class Decoes {
    public static string Deco(this PropertyInfo property) {
      var status = (property.CanRead ? "R" : " ") + (property.CanWrite ? "W" : " ");
      return $"[Property {status}] ({property.Name})";
    }
    public static string Deco<T>(this PropertyInfo property, T instance) {
      var rwStatus = (property.CanRead ? "R" : " ") + (property.CanWrite ? "W" : " ");
      var brief = $"[Property {rwStatus}] [{property.Name}]";
      try {
        if (property.CanRead) brief += $" ({property.GetValue(instance, property.GetIndexParameters())})";
      }
      catch (Exception e) {
        brief += $" (Err {e.HResult}: {e.Message})";
      }
      return brief;
    }
    public static string Deco(this ParameterInfo parameter) {
      return $"{parameter.ParameterType.Name} {parameter.Name}";
    }
    public static string Deco(this MethodInfo method) {
      var paramTypes = method.GetParameters().Join(p => p.Deco(), ", ");
      return $"{method.Name}({paramTypes}) => {method.ReturnParameter?.ParameterType.Name ?? "void"}";
    }
    public static string Deco<T>(this MethodInfo m, T instance) {
      var brief = m.Deco();
      try {
        var invokeValue = m.GetParameters().Length == 0 ? m.Invoke(instance, new object[] { }).ToString() : "";
        brief += invokeValue + ")";
      }
      catch (Exception e) {
        brief += $"(Err {e.HResult}: {e.Message})" + ")";
      }
      return brief;
    }
  }
}