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

namespace FacilityControl.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class CloseZone : ICommand
    {
        public string Command { get; set; } = "clozezone";

        public string[] Aliases { get; set; } = { "czone", "cz", };

        public string Description { get; set; } = "Closes all the doors in the specified zone. Does not lock them.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("fctrl.zones"))
            {
                response = "Access denied.";
                return false;
            }
            if (arguments.Count() < 1)
            {
                response = "Invalid format. Must be: \"blackoutzone light/heavy/entrance\" (eg. closezone light)";
                return false;
            }
            if (arguments.At(0).ToLower() != "light" && arguments.At(0).ToLower() != "heavy" && arguments.At(0).ToLower() != "entrance")
            {
                response = "First argument must be light, heavy, or entrance";
                return false;
            }
            ZoneType zone = (arguments.At(0).ToLower() == "light" ? ZoneType.LightContainment : (arguments.At(0).ToLower() == "heavy" ? ZoneType.HeavyContainment : (arguments.At(0).ToLower() == "entrance" ? ZoneType.Entrance : ZoneType.Unspecified)));
            foreach (Room r in Map.Rooms)
            {
                if (r.Zone == zone)
                {
                    foreach (Door d in r.Doors)
                    {
                        d.SetStateWithSound(false);
                    }
                }
            }
            response = $"Successfully closed all the doors in {zone.ToString()}";
            return true;
        }
    }
}
