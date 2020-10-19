using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandSystem;
using Exiled.Permissions.Extensions;
using Exiled.API.Enums;
using Exiled.API.Features;
using UnityEngine;

namespace FacilityControl.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class Escaping : ICommand
    {
        public string Command { get; set; } = "escaping";

        public string[] Aliases { get; set; } = { "esc" };

        public string Description { get; set; } = "Toggles escaping (if set to false, d class and scientists cannot escape). Note: Each player can only escape once, turning it off and back on will not allow players who passed the escape to escape.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("fctrl.settings"))
            {
                response = "Access denied.";
                return false;
            }
            if (arguments.Count() < 1)
            {
                response = "Proper usage: \"escaping (on/off)\"";
                return false;
            }
            if (arguments.At(0) != "on" && arguments.At(0) != "off")
            {
                response = "Provided argument must be \"on\" or \"off\".";
                return false;
            }
            FacilityControl.EscapingDisabled = (arguments.At(0) == "on" ? false : true);
            response = $"Successfully {(arguments.At(0) == "on" ? "enabled" : "disabled")} escaping.";
            return true;
        }
    }
}
