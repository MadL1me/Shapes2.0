namespace Test;

public interface IShape<TReturnType> where TReturnType : unmanaged
{
    TReturnType Area();
}

public interface IShape : IShape<double> { }

public record Triangle : IShape
{
    public double SideA;
    public double SideB;
    public double SideC;

    private Lazy<double> _lazyResult = new();

    public Triangle(double a, double b, double c)
    {
        if (a <= 0 || b <= 0 || c <= 0)
            throw new ArgumentOutOfRangeException("Side cannot be <= 0");

        if (a + b < c || a + c < b || c + b < a)
            throw new ArgumentException("The sum of two sides cannot be less then third side");

        SideA = a;
        SideB = b;
        SideC = c;

        _lazyResult = new(AreaInternal);
    }

    public double Area() => _lazyResult.Value;

    private double AreaInternal()
    {
        var p = (SideA + SideB + SideC) / 2;
        return Math.Sqrt(p * (p - SideA) * (p - SideB) * (p - SideC));
    }

    public bool IsRightAngled(double epsilon = 0.1)
    {
        return
            SideA.IsEqual(Math.Sqrt(Math.Pow(SideB, 2) + Math.Pow(SideC, 2)), epsilon) ||
            SideB.IsEqual(Math.Sqrt(Math.Pow(SideC, 2) + Math.Pow(SideA, 2)), epsilon) ||
            SideC.IsEqual(Math.Sqrt(Math.Pow(SideA, 2) + Math.Pow(SideB, 2)), epsilon);
    }
}

public record Circle : IShape<double>
{
    public double Radius;

    private Lazy<double> _lazyResult = new();

    public Circle(double radius)
    {
        if (radius <= 0)
            throw new ArgumentOutOfRangeException(nameof(radius), "Radius cannot be less or equal to 0");

        Radius = radius;

        _lazyResult = new(AreaInternal);
    }

    public double Area() => _lazyResult.Value;

    private double AreaInternal() => Radius * Radius * Math.PI;
}

public static class Extensions
{
    public static bool IsEqual(this double number, double comparable, double epsilon = 0.1) =>
        Math.Abs(number - comparable) <= epsilon;
}

/*

Задание по SQL:

SELECT p.id, p.name, coalesce(c.name, 'NO CATEGORY')
FROM "Product" as p
LEFT JOIN "ProductCategory" as pc on p.id = pc."prodId"
LEFT JOIN "Category" as c on c.id = pc."catId"

 */