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
    class DestroyDoor : ICommand
    {
        public string Command { get; set; } = "destroydoor";

        public string[] Aliases { get; set; } = { "dd" };

        public string Description { get; set; } = "Sets the targeted player to destroy every door they open/close (must have keycard access to do so) and pry gates.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("fctrl.destroydoor"))
            {
                response = "Access denied.";
                return false;
            }
            if (arguments.Count() < 2)
            {
                response = "Proper usage: dd (add/remove) (player)";
                return false;
            }
            if (arguments.At(0).ToLower() != "add" && arguments.At(0).ToLower() != "remove")
            {
                response = "Proper usage: dd (add/remove) (player)";
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
                if (arguments.At(0).ToLower() == "add" && !FacilityControl.PlySets["DestroyDoors"].Contains(Ply))
                {
                    FacilityControl.PlySets["DestroyDoors"].Add(Ply);
                    Affected.Add(Ply);
                }
                if (arguments.At(0).ToLower() == "remove" && FacilityControl.PlySets["DestroyDoors"].Contains(Ply))
                {
                    FacilityControl.PlySets["DestroyDoors"].Remove(Ply);
                    Affected.Add(Ply);
                }
            }
            response = $"Done! The request affected {Affected.Count()} players.";
            return false;
        }
    }
}
