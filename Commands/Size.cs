using CommandSystem;
using Exiled.API.Features;
using System;

namespace SCPSense
{
    internal class Size : ICommand
    {
        public string Command => "size";

        public static Size Instance { get; } = new Size();

        public string[] Aliases => new string[] { };

        public string Description => "Sets the size for SCPSense generated hints.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!int.TryParse(arguments.At(0), out var selected_size))
            {
                response = "Invalid value.";
                return false;
            }
            Player player = Player.Get(sender);
            if (player.DoNotTrack)
            {
                response = "Customization unavailable if user has DNT mode on.";
                return false;
            }
            if (Main.TextConfigs.TryGetValue(player.UserId, out var textConfig))
            {
                textConfig.size = selected_size;
            }
            else
            {
                Main.TextConfigs.Add(player.UserId, new TextConfig(selected_size));
            }
            Binary.WriteToBinaryFile(Main.filePath, Main.TextConfigs);
            response = "Changes applied.";
            return true;
        }
    }
}
