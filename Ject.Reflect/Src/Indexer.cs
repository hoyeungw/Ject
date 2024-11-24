using System;
using System.Reflection;
using Veho.Vector;

namespace Ject {
  public static class Indexer {
    public static MethodInfo[] StaticMethods(this Type algebra) => algebra.GetMethods(Bindings.StaticFlags);


    public static bool HasMethod(this Type algebra, string methodName) => algebra.StaticMethods().Some(it => it.Name == methodName);
    public static bool HasMethod(this Type algebra, string methodName, params Type[] parameterTypes) {
      try {
        var method = parameterTypes == null ? algebra.GetMethod(methodName) : algebra.GetMethod(methodName, parameterTypes);
        return method != null;
        //If True:  Debug.Print("{0}.{1}: {2} method", m.ReflectedType.Name, m.Name, If(m.IsStatic, "Static", "Instance"))
        //If False: Debug.Print("{0}.ToString method not found", algebra.Name)
      }
      catch (AmbiguousMatchException) {
        //Debug.Print("{0}.{1} has multiple public overloads.", algebra.Name, methodName)
        return false;
      }
    }
  }
}