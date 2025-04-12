using CommandSystem;

internal sealed class Escaped : ICommand
{
    public string Command { get; } = "escaped";
    public string[] Aliases { get; } = { };
    public string Description { get; } = "Escaped the facility.";

    public string Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        response = "You have escaped the facility!";
        return "success";
    }
} 