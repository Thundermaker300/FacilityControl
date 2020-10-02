using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorControl
{
    public class EventHandlers
    {
        // Server Events
        public void OnRoundStarted()
        {
            foreach (KeyValuePair<string, List<Player>> data in DoorControl.PlySets)
            {
                DoorControl.PlySets[data.Key].Clear();
            }
        }

        // Player Events
        public void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (ev.Player.ReferenceHub.isDedicatedServer) return;
            if (DoorControl.PlySets["NoInteractDoors"].Contains(ev.Player))
            {
                ev.IsAllowed = false;
                return;
            }
            if (DoorControl.PlySets["DestroyDoors"].Contains(ev.Player) && ev.IsAllowed)
            {
                if (ev.Door.doorType == Door.DoorTypes.HeavyGate)
                {
                    ev.IsAllowed = false;
                    ev.Door.PryGate();
                }
                else
                {
                    ev.Door.DestroyDoor(true);
                }
            }
        }
    }
}
