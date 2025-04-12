using CommandSystem;
using Exiled.Events.EventArgs.Player;

namespace RoleSwap.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    internal sealed class Main : ParentCommand
    {
        public Main() {LoadGeneratedCommands();}

        public override string Command => "RoleSwap";
        public override string[] Aliases => ["RS", "surrendering", "surrender", "bringus"];
        public override string Description => "Parent command for Role Swap";

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(Escaped.Instance);   
        }
    }
}