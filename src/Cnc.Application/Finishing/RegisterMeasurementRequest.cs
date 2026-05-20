namespace Cnc.Application.Finishing;

public sealed record RegisterMeasurementRequest(
    int StepNumber,
    double MeasuredDiameterMm
);
