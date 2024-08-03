namespace PrimatScheduleBot.Logging.Messages;

internal static class Errors
{
    public static string Unknown(string message, string? stacktrace) =>
        $"There is an error. Message: {message} \n Stacktrace: {stacktrace}";

    public static string FailedToStartService(string url) =>
        $"Failed to start {url}";

    public static string FailedToStopService(string url) =>
        $"Failed to stop {url}";
}
