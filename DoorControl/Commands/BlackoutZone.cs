using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandSystem;
using Exiled.Permissions.Extensions;
using Exiled.API.Enums;
using Exiled.API.Features;

namespace DoorControl.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class BlackoutZone : ICommand
    {
        public string Command { get; set; } = "blackoutzone";

        public string[] Aliases { get; set; } = { "bozone", "bzone", "bz", };

        public string Description { get; set; } = "Blacks out all the rooms in the specified zone, for the specified duration. Does not close/lock doors (only blacks out rooms)";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("dctrl.zones"))
            {
                response = "Access denied.";
                return false;
            }
            if (arguments.Count() < 2)
            {
                response = "Invalid format. Must be: \"blackoutzone light/heavy/entrance duration (eg. blackoutzone light 5)";
                return false;
            }
            if (arguments.At(0).ToLower() != "light" && arguments.At(0).ToLower() != "heavy" && arguments.At(0).ToLower() != "entrance" )
            {
                response = "First argument must be light, heavy, or entrance";
                return false;
            }
            ZoneType zone = (arguments.At(0).ToLower() == "light" ? ZoneType.LightContainment : (arguments.At(0).ToLower() == "heavy" ? ZoneType.HeavyContainment : (arguments.At(0).ToLower() == "entrance" ? ZoneType.Entrance : ZoneType.Unspecified)));
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
            foreach (Room r in Map.Rooms)
            {
                if (r.Zone == zone)
                {
                    r.TurnOffLights(length);
                }
            }
            response = $"Successfully blacked out all lights in {zone.ToString()}";
            return true;
        }
    }
}
