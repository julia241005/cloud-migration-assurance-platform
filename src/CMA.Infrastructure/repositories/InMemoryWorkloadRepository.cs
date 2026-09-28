using System.Collections.Concurrent;
using CMA.Application.Abstractions;
using CMA.Domain.Entities;

namespace CMA.Infrastructure.Repositories;

public sealed class InMemoryWorkloadRepository : IWorkloadRepository
{
    private readonly ConcurrentDictionary<Guid, Workload> _workloads = new();

    public Task AddAsync(
        Workload workload,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!_workloads.TryAdd(workload.Id, workload))
        {
            throw new InvalidOperationException(
                $"A workload com o ID '{workload.Id}' já existe.");
        }

        return Task.CompletedTask;
    }

    public Task<Workload?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _workloads.TryGetValue(id, out Workload? workload);

        return Task.FromResult(workload);
    }

    public Task<IEnumerable<Workload>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        // Returning .Values directly avoids the memory allocation of .ToList()
        return Task.FromResult<IEnumerable<Workload>>(_workloads.Values);
    }

    public Task UpdateAsync(
        Workload workload,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!_workloads.ContainsKey(workload.Id))
        {
            throw new KeyNotFoundException(
                $"Workload com o ID '{workload.Id}' não foi encontrado.");
        }

        _workloads[workload.Id] = workload;

        return Task.CompletedTask;
    }
}