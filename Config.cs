using Exiled.API.Interfaces;
using Exiled.API.Features;
using System.ComponentModel;
using System.IO;

namespace SCPSense
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        [Description("SCPs see each other's HP and AHP")]
        public bool SeeHP { get; set; } = true;
        [Description("SCPs can see the distance between them.")]
        public bool SeeDistance { get; set; } = true;
        [Description("Path for the plugin file. Required to change for Linux users as %AppData% is a windows only feature.")]
        public string SavePath { get; set; } = Path.Combine(Paths.AppData, @"EXILED\Configs\SCPSenseConfigs.bin");
    }
}
