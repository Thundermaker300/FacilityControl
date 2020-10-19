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
    class ToggleTeslas : ICommand
    {
        public string Command { get; set; } = "toggleteslas";

        public string[] Aliases { get; set; } = { "tt" };

        public string Description { get; set; } = "Toggles tesla gates on/off.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("fctrl.teslas"))
            {
                response = "Access denied.";
                return false;
            }
            if (arguments.Count() < 1)
            {
                response = "Proper usage: \"toggleteslas (on/off)\"";
                return false;
            }
            if (arguments.At(0) != "on" && arguments.At(0) != "off")
            {
                response = "Provided argument must be \"on\" or \"off\".";
                return false;
            }
            FacilityControl.TeslasDisabled = (arguments.At(0) == "on" ? false : true);
            response = $"Successfully {(arguments.At(0) == "on" ? "enabled" : "disabled")} tesla gates.";
            return true;
        }
    }
}
