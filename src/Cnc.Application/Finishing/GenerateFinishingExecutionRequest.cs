namespace Cnc.Application.Finishing;

public sealed record GenerateFinishingExecutionRequest(
    double StartDiameterMm,
    double TargetDiameterMm,
    int NumberOfCuts
);