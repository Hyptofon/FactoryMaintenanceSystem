namespace Application.Common.Settings;

public class ApplicationSettings
{
    public ConnectionStrings ConnectionStrings { get; set; } = new();
    public LoggingSettings Logging { get; set; } = new();
}

public class ConnectionStrings
{
    public string DefaultConnection { get; set; } = string.Empty;
}

public class LoggingSettings
{
    public LogLevel LogLevel { get; set; } = new();
}

public class LogLevel
{
    public string Default { get; set; } = "Information";
    public string MicrosoftAspNetCore { get; set; } = "Warning";
    public string MicrosoftEntityFrameworkCore { get; set; } = "Information";
}