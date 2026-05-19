using Cnc.Application.Finishing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // kan vente foreløpig

app.MapPost("/api/finishing-executions", (
    GenerateFinishingExecutionRequest request) =>
{
    var handler = new GenerateFinishingExecutionHandler();

    var execution = handler.Handle(request);

    return Results.Ok(new
    {
        execution.Id,
        Steps = execution.Steps.Select(step => new
        {
            step.StepNumber,
            PlannedDiameterMm = step.PlannedDiameter.Value,
            MeasuredDiameterMm = step.MeasuredDiameter?.Value,
            step.IsLocked
        })
    });
})
.WithName("GenerateFinishingExecution")
.WithOpenApi();

app.Run();