using static Generic.Math.GenericMath;

namespace Ject.Test.Static {
  public struct Point<T> {
    public T X;
    public T Y;
    public static Point<T> Build(T x, T y) {
      return new Point<T> { X = x, Y = y };
    }
    public override string ToString() { return $"({X}, {Y})"; }
    public static Point<T> operator +(Point<T> a, Point<T> b) => Point<T>.Build(Add(a.X, b.X), Add(a.Y, b.Y));
    public static Point<T> operator -(Point<T> a, Point<T> b) => Point<T>.Build(Subtract(a.X, b.X), Subtract(a.Y, b.Y));
    public static Point<T> operator *(Point<T> a, Point<T> b) => Point<T>.Build(Multiply(a.X, b.X), Multiply(a.Y, b.Y));
    public static Point<T> operator /(Point<T> a, Point<T> b) => Point<T>.Build(Divide(a.X, b.X), Divide(a.Y, b.Y));
    public static Point<T> operator +(Point<T> a, T b) => Point<T>.Build(Add(a.X, b), Add(a.Y, b));
    public static Point<T> operator -(Point<T> a, T b) => Point<T>.Build(Subtract(a.X, b), Subtract(a.Y, b));
    public static Point<T> operator *(Point<T> a, T b) => Point<T>.Build(Multiply(a.X, b), Multiply(a.Y, b));
    public static Point<T> operator /(Point<T> a, T b) => Point<T>.Build(Divide(a.X, b), Divide(a.Y, b));
  }
}