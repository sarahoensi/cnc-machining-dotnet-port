using Cnc.Domain.Finishing;

namespace Cnc.Application.Finishing;

public sealed record GenerateFinishingExecutionRequest(
    FinishingMode Mode,
    double StartDiameterMm,
    double TargetDiameterMm,
    int NumberOfCuts
);
