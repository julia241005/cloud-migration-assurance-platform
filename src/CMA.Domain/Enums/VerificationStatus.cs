using System;
using System.Collections.Generic;
using System.Text;

namespace CMA.Domain.Enums;

public enum VerificationStatus
{
    Pending = 0,    // Aguardando verificação
    Healthy = 1,    // Todos os testes passaram
    Degraded = 2,   // Parte dos testes falhou
    Unhealthy = 3   // Falha crítica de acessibilidade
}