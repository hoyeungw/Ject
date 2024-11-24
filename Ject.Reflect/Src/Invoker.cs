using System.Reflection;

namespace Ject {
  public static class Invoker {
    public static dynamic StaticInvoke(this MethodInfo method, params dynamic[] parameters) {
      return method.Invoke(null, parameters);
    }
    public static T StaticInvoke<T>(this MethodInfo method, params dynamic[] parameters) {
      return (T)method.Invoke(null, parameters);
    }
    
  }
}