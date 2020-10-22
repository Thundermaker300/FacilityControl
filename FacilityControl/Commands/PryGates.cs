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
    class PryGates : ICommand
    {
        public string Command { get; set; } = "prygates";

        public string[] Aliases { get; set; } = { };

        public string Description { get; set; } = "Allows the targeted player to pry open any heavy gate they interact with.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("fctrl.prygates"))
            {
                response = "Access denied.";
                return false;
            }
            if (arguments.Count() < 2)
            {
                response = "Proper usage: prygates (add/remove) (player)";
                return false;
            }
            if (arguments.At(0).ToLower() != "add" && arguments.At(0).ToLower() != "remove")
            {
                response = "Proper usage: prygates (add/remove) (player)";
                return false;
            }
            List<Player> Plys = API.GetPlayers(arguments.At(1));
            if (Plys.Count() == 0)
            {
                response = "Invalid player(s).";
                return false;
            }
            List<Player> Affected = new List<Player> { };
            foreach (Player Ply in Plys)
            {
                if (arguments.At(0).ToLower() == "add" && !FacilityControl.PlySets["PryGates"].Contains(Ply))
                {
                    FacilityControl.PlySets["PryGates"].Add(Ply);
                    Affected.Add(Ply);
                }
                if (arguments.At(0).ToLower() == "remove" && FacilityControl.PlySets["PryGates"].Contains(Ply))
                {
                    FacilityControl.PlySets["PryGates"].Remove(Ply);
                    Affected.Add(Ply);
                }
            }
            response = $"Done! The request affected {Affected.Count()} players.";
            return false;
        }
    }
}
