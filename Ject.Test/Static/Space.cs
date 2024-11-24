using System;
using System.Linq;
using System.Reflection;

namespace Ject.Test.Static {
  public class Space {
    public Type Algebra;
    public MethodInfo[] Methods => Algebra.GetMethods();
    public ILookup<string, MethodInfo> MethodLookup;

    public static Space Build(Type algebra) {
      var lookup = algebra.GetMethods().ToLookup(m => m.Name);
      return new Space { Algebra = algebra, MethodLookup = lookup };
    }

    public bool TryFind(string name, out MethodInfo method) {
      return (method = Methods.FirstOrDefault(it => it.Name == name)) != null;
    }

    public bool TryFind(string name, Type[] paramTypes, out MethodInfo method) {
      // Console.WriteLine($">> TryParseMethod on [algebra] ({algebra}) for [method] ({name}) {paramTypes?.Deco()}");
      try {
        if (paramTypes == null) return this.TryFind(name, out method);
        method = Algebra.GetMethod(name, paramTypes);
      }
      catch (AmbiguousMatchException) {
        method = null;
      }
      return method != null;
    }
  }
}