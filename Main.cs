using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.IO;
using Server = Exiled.Events.Handlers.Server;

namespace SCPSense
{
    public class Main : Plugin<Config>
    {
        EventHandlers handlers = new();
        internal static Dictionary<string, TextConfig> TextConfigs = new();
        internal static string filePath = Environment.ExpandEnvironmentVariables(@"%appdata%\EXILED\Configs\SCPSenseConfigs.bin");
        public static Main Instance { get; set; }
        private void GetOrCreateTextConfig()
        {
            if (!File.Exists(filePath))
            {
                Binary.WriteToBinaryFile(filePath, TextConfigs);
                return;
            }
            TextConfigs = Binary.ReadFromBinaryFile<Dictionary<string, TextConfig>>(filePath);
        }
        public override void OnEnabled()
        {
            Instance = this;
            GetOrCreateTextConfig();
            handlers = new();
            Server.RoundStarted += handlers.OnRoundStart;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            handlers = null;
            Server.RoundStarted -= handlers.OnRoundStart;
            base.OnDisabled();
        }
    }
}