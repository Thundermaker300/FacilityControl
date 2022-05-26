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

        public string[] Aliases { get; set; } = {  };

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
            IEnumerable<Door> doors = new List<Door>();
            switch (zone)
            {
                case ZoneType.LightContainment:
                    doors = Door.List.Where(door => door.Nametag == "CHECKPOINT_LCZ_A" || door.Nametag == "CHECKPOINT_LCZ_B");
                    break;
                case ZoneType.HeavyContainment:
                    doors = Door.List.Where(door => door.Nametag == "CHECKPOINT_LCZ_A" || door.Nametag == "CHECKPOINT_LCZ_B" || door.Nametag == "CHECKPOINT_EZ_HCZ");
                    break;
                case ZoneType.Entrance:
                    doors = Door.List.Where(door => door.Nametag == "GATE_A" || door.Nametag == "GATE_B" || door.Nametag == "CHECKPOINT_EZ_HCZ");
                    break;
                case ZoneType.Surface:
                    doors = Door.List.Where(door => door.Nametag == "GATE_A" || door.Nametag == "GATE_B");
                    break;
            }
            foreach (Door door in doors)
            {
                door.IsOpen = false;
                door.ChangeLock(DoorLockType.AdminCommand);
            }
            Timing.CallDelayed(length, () =>
            {
                foreach (Door door in doors)
                {
                    door.ChangeLock(DoorLockType.None);
                }
            });
            response = $"Successfully isolated {(zone == ZoneType.Surface ? "the facility" : zone.ToString())}";
            return true;
        }
    }
}
