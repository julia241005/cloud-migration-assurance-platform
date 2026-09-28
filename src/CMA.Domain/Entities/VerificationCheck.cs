using System;
using System.Collections.Generic;
using System.Text;

namespace CMA.Domain.Entities;

public class VerificationCheck
{
    public Guid Id { get; private set; }
    public string CheckName { get; private set; }
    public bool Passed { get; private set; }
    public string Details { get; private set; }
    public DateTime ExecutedAt { get; private set; }

    public VerificationCheck(string checkName, bool passed, string details)
    {
        Id = Guid.NewGuid();
        CheckName = checkName;
        Passed = passed;
        Details = details;
        ExecutedAt = DateTime.UtcNow;
    }
}