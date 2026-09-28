using CMA.Application.Abstractions;
using CMA.Domain.Entities;
using CMA.Domain.Enums;
using System.Diagnostics;

namespace CMA.Application.Workloads.Commands;

public class RunWorkloadVerificationUseCase
{
    private readonly IWorkloadRepository _repository;
    private readonly HttpClient _httpClient;

    public RunWorkloadVerificationUseCase(IWorkloadRepository repository, HttpClient httpClient)
    {
        _repository = repository;
        _httpClient = httpClient;
    }

    public async Task<Workload?> ExecuteAsync(Guid workloadId, CancellationToken cancellationToken = default)
    {
        // 1. Vai buscar o Workload de forma assíncrona
        var workload = await _repository.GetByIdAsync(workloadId);

        if (workload == null)
            return null;

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        try
        {
            // 2. Faz o pedido GET ao URL do Workload
            var response = await _httpClient.GetAsync(workload.TargetUrl, cancellationToken);
            stopwatch.Stop();

            // 3. Valida se o status HTTP foi bem-sucedido
            var passed = response.IsSuccessStatusCode;
            var details = $"HTTP Status: {(int)response.StatusCode} {response.ReasonPhrase}. Response Time: {stopwatch.ElapsedMilliseconds}ms";

            // 4. Adiciona o resultado (a entidade calcula o status internamente)
            workload.AddCheckResult("HTTP Endpoint Reachability", passed, details);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            workload.AddCheckResult("HTTP Endpoint Reachability", false, $"Exception: {ex.Message}");
        }

        // 5. Guarda no repositório de forma assíncrona
        await _repository.UpdateAsync(workload);

        return workload;
    }
}