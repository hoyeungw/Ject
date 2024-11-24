using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Ject {
  public static class Convert {
    public static bool TryParseMethod(this Type algebra, string name, Type[] paramTypes, out MethodInfo method) {
      // Console.WriteLine($">> TryParseMethod on [algebra] ({algebra}) for [method] ({name}) {paramTypes?.Deco()}");
      try {
        if (paramTypes == null) return algebra.TryParseMethod(name, out method);
        method = algebra.GetMethod(name, paramTypes);
      }
      catch (AmbiguousMatchException) {
        method = null;
      }
      return method != null;
    }
    public static bool TryParseMethod(this Type algebra, string name, out MethodInfo method) {
      // Console.WriteLine(algebra.GetMethods().Map((i, x) => (i, x)).DecoEntries(presets: (Presets.Metro, Presets.Fresh)));
      method = algebra.GetMethods().FirstOrDefault(it => it.Name == name);
      // Console.WriteLine($">> [method] ({name}) {method}");
      return method != null;
    }
    public static bool TryParseMethod(this Type algebra, Regex regex, out MethodInfo method) {
      // Console.WriteLine(algebra.GetMethods().Map((i, x) => (i, x)).DecoEntries(presets: (Presets.Metro, Presets.Fresh)));
      method = algebra.GetMethods().FirstOrDefault(it => regex.IsMatch(it.Name));
      // Console.WriteLine($">> [method] ({name}) {method}");
      return method != null;
    }
  }
}