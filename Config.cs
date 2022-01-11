using Exiled.API.Interfaces;
using System.ComponentModel;

namespace SCPSense
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        [Description("SCPs see each other's HP and AHP")]
        public bool SeeHP { get; set; } = true;
        [Description("SCPs can see the distance between them.")]
        public bool SeeDistance { get; set; } = true;
    }
}
