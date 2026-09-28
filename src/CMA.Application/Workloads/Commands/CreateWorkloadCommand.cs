namespace CMA.Application.Workloads.Commands;

public record CreateWorkloadCommand(
    string Name,
    string Description,
    string Environment,
    string TargetUrl);