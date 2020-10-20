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
    class KillClassZone : ICommand
    {
        public string Command { get; set; } = "killclasszone";

        public string[] Aliases { get; set; } = { "kczone", "kcz", };

        public string Description { get; set; } = "Kills all players of a specific class in the specified zone.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("fctrl.killzone"))
            {
                response = "Access denied.";
                return false;
            }
            if (arguments.Count() < 2)
            {
                response = "Invalid format. Must be: \"killclasszone number light/heavy/entrance/surface (eg. killzone light)";
                return false;
            }
            if (arguments.At(1).ToLower() != "light" && arguments.At(1).ToLower() != "heavy" && arguments.At(1).ToLower() != "entrance" && arguments.At(1).ToLower() != "surface")
            {
                response = "First argument must be light, heavy, entrance, or surface";
                return false;
            }
            ZoneType zone = (arguments.At(1).ToLower() == "light" ? ZoneType.LightContainment : (arguments.At(1).ToLower() == "heavy" ? ZoneType.HeavyContainment : (arguments.At(1).ToLower() == "entrance" ? ZoneType.Entrance : (arguments.At(1).ToLower() == "surface" ? ZoneType.Surface : ZoneType.Unspecified))));
            int totalKilled = 0;
            if (!Enum.TryParse(arguments.At(0), true, out RoleType role))
            {
                response = "Invalid role provided";
                return false;
            }
            foreach (Player Ply in Player.List.Where(P => P.Role == role))
            {
                if (Ply.CurrentRoom.Zone == zone && !Ply.IsGodModeEnabled)
                {
                    Ply.Hurt(99999, DamageTypes.Wall, "ZONEKILL");
                    totalKilled++;
                }
            }
            response = $"Killed {totalKilled} players in {zone.ToString()}";
            return true;
        }
    }
}
