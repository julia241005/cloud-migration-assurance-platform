using CMA.Application.Abstractions;
using CMA.Domain.Entities;

namespace CMA.Application.Workloads.Queries;

public sealed class GetWorkloadByIdUseCase
{
    private readonly IWorkloadRepository _repository;

    public GetWorkloadByIdUseCase(IWorkloadRepository repository)
    {
        _repository = repository;
    }

    public Task<Workload?> ExecuteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return _repository.GetByIdAsync(id, cancellationToken);
    }
}