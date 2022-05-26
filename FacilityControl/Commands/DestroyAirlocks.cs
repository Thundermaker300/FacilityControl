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
using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;

namespace FacilityControl.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class DestroyAirlocks : ICommand
    {
        public string Command { get; set; } = "destroyairlocks";

        public string[] Aliases { get; set; } = { "dair" };

        public string Description { get; set; } = "Destroy airlocks.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("fctrl.airlocks"))
            {
                response = "Access denied.";
                return false;
            }
            foreach (var airlock in GameObject.FindObjectsOfType<AirlockController>())
            {
                (airlock._doorA as IDamageableDoor).ServerDamage(9999f, DoorDamageType.ServerCommand);
                (airlock._doorB as IDamageableDoor).ServerDamage(9999f, DoorDamageType.ServerCommand);
            }
            response = $"rip";
            return true;
        }
    }
}
