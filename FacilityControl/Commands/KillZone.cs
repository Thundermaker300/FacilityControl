using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandSystem;
using Exiled.Permissions.Extensions;
using Exiled.API.Enums;
using Exiled.API.Features;
using Respawning;

namespace FacilityControl.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class KillZone : ICommand
    {
        public string Command { get; set; } = "killzone";

        public string[] Aliases { get; set; } = { "kzone", "kz", };

        public string Description { get; set; } = "Kills all players in the specified zone.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("fctrl.killzone"))
            {
                response = "Access denied.";
                return false;
            }
            if (arguments.Count() < 1)
            {
                response = "Invalid format. Must be: \"killzone light/heavy/entrance/surface (eg. killzone light)";
                return false;
            }
            if (arguments.At(0).ToLower() != "light" && arguments.At(0).ToLower() != "heavy" && arguments.At(0).ToLower() != "entrance" && arguments.At(0).ToLower() != "surface")
            {
                response = "First argument must be light, heavy, entrance, or surface";
                return false;
            }
            ZoneType zone = (arguments.At(0).ToLower() == "light" ? ZoneType.LightContainment : (arguments.At(0).ToLower() == "heavy" ? ZoneType.HeavyContainment : (arguments.At(0).ToLower() == "entrance" ? ZoneType.Entrance : (arguments.At(0).ToLower() == "surface" ? ZoneType.Surface : ZoneType.Unspecified))));
            int totalKilled = 0;
            foreach (Player Ply in Player.List)
            {
                if (Ply.CurrentRoom.Zone == zone && !Ply.IsGodModeEnabled)
                {
                    Ply.Hurt(99999, "Zone purged");
                    totalKilled++;
                }
            }
            response = $"Killed {totalKilled} players in {zone.ToString()}";
            return true;
        }
    }
}
