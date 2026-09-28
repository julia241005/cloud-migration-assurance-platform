
using CMA.Application.Workloads.Commands;
using CMA.Application.Workloads.Queries;
using CMA.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configuração de Controllers
builder.Services.AddControllers();

// Injeção de Dependência (DI)
builder.Services.AddSingleton<IWorkloadRepository, InMemoryWorkloadRepository>();
builder.Services.AddScoped<CreateWorkloadUseCase>();
builder.Services.AddScoped<GetWorkloadByIdUseCase>();
builder.Services.AddScoped<GetAllWorkloadsUseCase>();
// Configuração de OpenAPI/Swagger
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// ADICIONE ESTA LINHA AQUI:
app.UseStaticFiles();

app.UseAuthorization();
app.MapControllers();

app.Run();