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
        // 1. Vai buscar o Workload (ajuste para GetByIdAsync ou GetById conforme o seu repositório)
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

            // 4. Adiciona o resultado (a entidade já calcula o status e atualiza a data internamente)
            workload.AddCheckResult("HTTP Endpoint Reachability", passed, details);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            // Em caso de erro de rede/exceção
            workload.AddCheckResult("HTTP Endpoint Reachability", false, $"Exception: {ex.Message}");
        }

        // 5. Guarda no repositório (use _repository.Update(workload) ou o método síncrono/assíncrono que tiver definido na sua interface)
        _repository.Update(workload);

        return workload;
    }
}