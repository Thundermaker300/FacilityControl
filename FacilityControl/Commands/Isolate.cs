using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandSystem;
using Exiled.Permissions.Extensions;
using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;

namespace FacilityControl.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class Isolate : ICommand
    {
        public string Command { get; set; } = "isolate";

        public string[] Aliases { get; set; } = { "isolate" };

        public string Description { get; set; } = "Locks the checkpoints & gates of the specified zone for the specified duration. Use \"facility\" in replacement of a zone to lock gate A and B (no checkpoints).";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("fctrl.zones"))
            {
                response = "Access denied.";
                return false;
            }
            if (arguments.Count() < 2)
            {
                response = "Invalid format. Must be: \"isolate light/heavy/entrance duration (eg. isolate light 5)";
                return false;
            }
            if (arguments.At(0).ToLower() != "light" && arguments.At(0).ToLower() != "heavy" && arguments.At(0).ToLower() != "entrance" && arguments.At(0).ToLower() != "facility")
            {
                response = "First argument must be light, heavy, entrance, or facility";
                return false;
            }
            ZoneType zone = (arguments.At(0).ToLower() == "light" ? ZoneType.LightContainment : (arguments.At(0).ToLower() == "heavy" ? ZoneType.HeavyContainment : (arguments.At(0).ToLower() == "entrance" ? ZoneType.Entrance : (arguments.At(0).ToLower() == "facility" ? ZoneType.Surface : ZoneType.Unspecified))));
            int length;
            try
            {
                length = Convert.ToInt32(arguments.At(1));
            }
            catch
            {
                response = "Second argument must be a valid number (duration)";
                return false;
            }
            List<Door> doors = new List<Door> { };
            switch (zone)
            {
                case ZoneType.LightContainment:
                    doors = Map.Doors.ToList().FindAll(door => door.DoorName == "CHECKPOINT_LCZ_A" || door.DoorName == "CHECKPOINT_LCZ_B");
                    break;
                case ZoneType.HeavyContainment:
                    doors = Map.Doors.ToList().FindAll(door => door.DoorName == "CHECKPOINT_LCZ_A" || door.DoorName == "CHECKPOINT_LCZ_B" || door.DoorName == "CHECKPOINT_ENT");
                    break;
                case ZoneType.Entrance:
                    doors = Map.Doors.ToList().FindAll(door => door.DoorName == "GATE_A" || door.DoorName == "GATE_B" || door.DoorName == "CHECKPOINT_ENT");
                    break;
                case ZoneType.Surface:
                    doors = Map.Doors.ToList().FindAll(door => door.DoorName == "GATE_A" || door.DoorName == "GATE_B");
                    break;
            }
            foreach (Door door in doors)
            {
                door.SetStateWithSound(false);
                door.SetLock(true);
            }
            Timing.CallDelayed(length, () =>
            {
                foreach (Door door in doors)
                {
                    door.SetLock(false);
                }
            });
            response = $"Successfully isolated {(zone == ZoneType.Surface ? "the facility" : zone.ToString())}";
            return true;
        }
    }
}
