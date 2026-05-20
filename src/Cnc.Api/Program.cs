using Cnc.Application.Finishing;
using Cnc.Domain.Finishing;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IFinishingExecutionStore, InMemoryFinishingExecutionStore>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/api/finishing-executions", (
    GenerateFinishingExecutionRequest request,
    IFinishingExecutionStore store) =>
{
    var handler = new GenerateFinishingExecutionHandler();

    var execution = handler.Handle(request);
    store.Save(execution);

    return Results.Ok(MapExecutionResponse(execution));
})
.WithName("GenerateFinishingExecution")
.WithOpenApi();

app.MapPost("/api/finishing-executions/{id:guid}/measurements", (
    Guid id,
    RegisterMeasurementRequest request,
    IFinishingExecutionStore store) =>
{
    var handler = new RegisterMeasurementHandler(store);

    try
    {
        var execution = handler.Handle(id, request);
        return Results.Ok(MapExecutionResponse(execution));
    }
    catch (KeyNotFoundException)
    {
        return Results.NotFound(new { Message = "Execution not found." });
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { ex.Message });
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(new { ex.Message });
    }
})
.WithName("RegisterFinishingMeasurement")
.WithOpenApi();

app.Run();

static object MapExecutionResponse(FinishingExecution execution) =>
    new
    {
        execution.Id,
        execution.Mode,
        Steps = execution.Steps.Select(step => new
        {
            step.StepNumber,
            PlannedDiameterMm = step.PlannedDiameter.Value,
            MeasuredDiameterMm = step.MeasuredDiameter?.Value,
            step.IsLocked
        })
    };
