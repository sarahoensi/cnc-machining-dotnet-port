using Cnc.Domain.Finishing;

namespace Cnc.Application.Finishing;

public sealed class GenerateFinishingExecutionHandler
{
    public FinishingExecution Handle(GenerateFinishingExecutionRequest request)
    {
        return FinishingPlanGenerator.Generate(
            new Diameter(request.StartDiameterMm),
            new Diameter(request.TargetDiameterMm),
            request.NumberOfCuts
        );
    }
}