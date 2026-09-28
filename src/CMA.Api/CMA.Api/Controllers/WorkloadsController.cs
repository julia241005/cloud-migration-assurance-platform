using CMA.Application.Workloads.Commands;
using CMA.Application.Workloads.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMA.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class WorkloadsController : ControllerBase
{
    private readonly CreateWorkloadUseCase _createWorkloadUseCase;
    private readonly GetWorkloadByIdUseCase _getWorkloadByIdUseCase;
    private readonly GetAllWorkloadsUseCase _getAllWorkloadsUseCase;

    public WorkloadsController(
        CreateWorkloadUseCase createWorkloadUseCase,
        GetWorkloadByIdUseCase getWorkloadByIdUseCase,
        GetAllWorkloadsUseCase getAllWorkloadsUseCase)
    {
        _createWorkloadUseCase = createWorkloadUseCase;
        _getWorkloadByIdUseCase = getWorkloadByIdUseCase;
        _getAllWorkloadsUseCase = getAllWorkloadsUseCase;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateWorkloadCommand command,
        CancellationToken cancellationToken)
    {
        var workloadId = await _createWorkloadUseCase.ExecuteAsync(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = workloadId },
            new { id = workloadId });
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var workloads = await _getAllWorkloadsUseCase.ExecuteAsync(
            cancellationToken);

        return Ok(workloads);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var workload = await _getWorkloadByIdUseCase.ExecuteAsync(
            id,
            cancellationToken);

        if (workload is null)
        {
            return NotFound();
        }

        return Ok(workload);
    }

    [HttpPost("{id:guid}/verify")]
    public async Task<IActionResult> RunVerification(
        Guid id,
        [FromServices] RunWorkloadVerificationUseCase useCase)
    {
        var workload = await useCase.ExecuteAsync(id);

        if (workload == null)
            return NotFound(new { message = $"Workload with ID {id} not found." });

        return Ok(workload);
    }
}