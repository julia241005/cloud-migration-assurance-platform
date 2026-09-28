using CMA.Application.Abstractions;
using CMA.Domain.Entities;

namespace CMA.Application.Workloads.Queries;

public sealed class GetAllWorkloadsUseCase
{
    private readonly IWorkloadRepository _repository;

    public GetAllWorkloadsUseCase(IWorkloadRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Workload>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}