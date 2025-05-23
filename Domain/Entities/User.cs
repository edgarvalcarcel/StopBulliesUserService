﻿using Microsoft.AspNetCore.Identity;

namespace StopBulliesUserService.Domain.Entities;

internal sealed class User : IdentityUser
{
    public bool Locked { get; set; }
    public int StatusId { get; set; } = 1;
    public Status? Status { get; set; }
    public DateTime PasswordExpDate { get; set; } = DateTime.UtcNow.AddDays(75);
    public DateTimeOffset Created { get; set; }
    public string? CreatedBy { get; set; }
}
