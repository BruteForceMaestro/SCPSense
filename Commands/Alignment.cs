using CommandSystem;
using Exiled.API.Features;
using System;

namespace SCPSense
{
    internal class Alignment : ICommand
    {
        public string Command => "alignment";
        public static Alignment Instance { get; set; } = new Alignment();
        public string[] Aliases => new string[] { "align" };

        public string Description => "sets alignment for the text. refer to unity rich text for picking";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.At(0) != "left" && arguments.At(0) != "right" && arguments.At(0) != "center" && arguments.At(0) != "justified" && arguments.At(0) != "flush")
            {
                response = "invalid alignment chosen. refer to unity rich text for more information.";
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
                textConfig.align = arguments.At(0);
            }
            else
            {
                Main.TextConfigs.Add(player.UserId, new TextConfig(arguments.At(0)));
            }
            Binary.WriteToBinaryFile(Main.Instance.Config.SavePath, Main.TextConfigs);
            response = "Changes applied.";
            return true;
        }
    }
}
