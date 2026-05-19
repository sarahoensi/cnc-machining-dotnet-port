namespace Cnc.Domain.Finishing;

public sealed record Diameter
{
    public double Value { get; }

    public Diameter(double value)
    {
        if (double.IsNaN(value) || double.IsInfinity(value) || value <= 0)
        {
            throw new ArgumentException("Diameter must be positive and finite.");
        }

        Value = value;
    }
}