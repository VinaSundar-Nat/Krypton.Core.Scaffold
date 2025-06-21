using System;

namespace Kr.__PROJECT_NAME__.Domain.Models.Infrastructure;

public sealed class KerstalConfiguration 
{
    public const string HostingOptions = "Host";
    public bool UseKerstal { get; set; }
    public string? CertPath { get; set; }
    public string? CertPassword { get; set; }
    public int Port { get; set; }
}
