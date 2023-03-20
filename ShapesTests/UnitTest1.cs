using Test;

namespace ShapesTests;

public class Tests
{
    [Theory]
    [InlineData(100, 1, 2)]
    [InlineData(3, 119, 123)]
    public void TriangleInvalidParamsTest(double a, double b, double c)
    {
        Assert.Throws<ArgumentException>(() => new Triangle(a,b,c));
    }

    [Theory]
    [InlineData(0, 1, 2)]
    [InlineData(-3, 5, 7)]
    public void TriangleInvalidParamsTestOutOfRange(double a, double b, double c)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Triangle(a, b, c));
    }


    [Theory]
    [InlineData(3, 4, 5, true)]
    [InlineData(10, 5, 8.66, true)]
    [InlineData(1, 2, 3, false)]
    public void TriangleIsRightAngleTest(double a, double b, double c, bool isRightAngled)
    {
        Assert.Equal(isRightAngled, new Triangle(a, b, c).IsRightAngled());
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void CircleInvalidParamsTest(double radius)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Circle(radius));
    }

    [Theory]
    [InlineData(1, 2, 2, 0.9682)]
    [InlineData(1, 3, 3, 1.479019)]
    [InlineData(8, 10, 5, 19.81003)]
    public void TriangleGetAreaTest(double a, double b, double c, double area)
    {
        Assert.True(area.IsEqual(new Triangle(a, b, c).Area()));
    }

    [Theory]
    [InlineData(1, 3.1415)]
    [InlineData(2, 12.5663)]
    [InlineData(10, 314.159)]
    public void CircleGetAreaTest(double radius, double area)
    {
        Assert.True(area.IsEqual(new Circle(radius).Area()));
    }
}