using System;
using System.Reflection;
using Veho.Vector;

namespace Ject {
  public static class Param {
    public static Type[] ParamTypes(this MethodInfo method) =>
      method.GetParameters().Map(it => it.ParameterType);
  }
}