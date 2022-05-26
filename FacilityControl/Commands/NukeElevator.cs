/*using System;
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
    class NukeElevator : ICommand
    {
        public string Command { get; set; } = "nukeelevator";

        public string[] Aliases { get; set; } = { "nukeelev", "nelev" };

        public string Description { get; set; } = "Disables/Enables the elevator leading to the alpha warhead switch. Useful if you're hosting an event involving the warhead and you don't want someone turning it off. Notice: Players inside the elevator may get stuck when this is used!";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("fctrl.elevators"))
            {
                response = "Access denied.";
                return false;
            }
            if (arguments.Count() < 1)
            {
                response = "Proper usage: \"nukeelevator (on/off)\"";
                return false;
            }
            if (arguments.At(0) != "on" && arguments.At(0) != "off")
            {
                response = "Provided argument must be \"on\" or \"off\".";
                return false;
            }
            foreach (Lift l in Map.Lifts.Where(i => i.elevatorName == ""))
            {
                l.SetLock(arguments.At(0) == "on" ? false : true);
                l.SetStatus((byte)1);
                l.UseLift();
            }
            response = $"Successfully {(arguments.At(0) == "on" ? "enabled" : "disabled")} the nuke elevator.";
            return true;
        }
    }
}
*/