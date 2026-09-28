using CMA.Domain.Entities;

namespace CMA.Application.Abstractions;

public interface IWorkloadRepository
{
    Task AddAsync(Workload workload, CancellationToken cancellationToken = default);
    Task<Workload?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Workload>> GetAllAsync(CancellationToken cancellationToken = default);
}