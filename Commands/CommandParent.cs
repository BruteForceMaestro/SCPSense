using CommandSystem;
using System;

namespace SCPSense
{
    [CommandHandler(typeof(ClientCommandHandler))]
    internal class CommandParent : ParentCommand
    {
        public CommandParent() => LoadGeneratedCommands();
        public override string Command => "SCPSense";

        public override string[] Aliases => new string[] { "ss" };

        public override string Description => "Allows to modify size and alignment of SCPSense generated text.";

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(Size.Instance);
            RegisterCommand(Alignment.Instance);
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "Use: .scpsense (size | align)";
            return false;
        }
    }
}
