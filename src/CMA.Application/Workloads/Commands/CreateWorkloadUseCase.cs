using CMA.Application.Abstractions;
using CMA.Domain.Entities;

namespace CMA.Application.Workloads.Commands;

public class CreateWorkloadUseCase
{
    private readonly IWorkloadRepository _repository;

    public CreateWorkloadUseCase(IWorkloadRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> ExecuteAsync(CreateWorkloadCommand command, CancellationToken cancellationToken = default)
    {
        // Agora passando os novos parâmetros exigidos pelo construtor
        var workload = new Workload(
            command.Name,
            command.Description,
            command.Environment,
            command.TargetUrl);

        await _repository.AddAsync(workload, cancellationToken);

        return workload.Id;
    }
}