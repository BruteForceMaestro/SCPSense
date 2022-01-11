using MEC;
using Exiled.Events.EventArgs;

namespace SCPSense
{
    internal class EventHandlers
    {
        public void OnRoundStart()
        {
            Timing.RunCoroutine(API.InfoLoop());
        }
    }
}
