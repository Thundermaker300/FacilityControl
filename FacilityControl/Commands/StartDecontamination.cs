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
    class StartDecontamination : ICommand
    {
        public string Command { get; set; } = "startdecontamination";

        public string[] Aliases { get; set; } = { "startdecon", "sdec" };

        public string Description { get; set; } = "Forces light containment zone decontamination to begin. This cannot be undone!";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("fctrl.sdecon"))
            {
                response = "Access denied.";
                return false;
            }
            if (Map.IsLCZDecontaminated)
            {
                response = "Decontamination has already begun.";
                return false;
            }
            Map.StartDecontamination();
            response = "Decontamination has begun.";
            return true;
        }
    }
}
