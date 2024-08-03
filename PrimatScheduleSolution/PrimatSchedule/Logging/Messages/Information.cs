namespace PrimatScheduleBot.Logging.Messages;

internal static class Information
{
    public static string ServiceStarted(string url) =>
        $"{url} is started";

    public static string ServiceStopped(string url) =>
        $"{url} is stopped";

    public static string FetchingUpdateHandler(string updateType) =>
        $"Fetching a typed handler for the update type {updateType}";

    public static string UnknownUpdate(string updateType) =>
        $"The update of the type {updateType} is unknown";

    public static string MessageReceived(string? message, long chatId) =>
        $"Received a message \"{message ?? string.Empty}\" from the chat with the id {chatId}";
}
