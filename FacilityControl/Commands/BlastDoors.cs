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
using MEC;

namespace FacilityControl.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class BlastDoors : ICommand
    {
        public string Command { get; set; } = "blastdoors";

        public string[] Aliases { get; set; } = { "blastd" };

        public string Description { get; set; } = "Forces the blast doors over Gate A and Gate B's elevators to close. Does not lock the elevators or activate the nuke. These doors CANNOT be reopened.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("fctrl.zones"))
            {
                response = "Access denied.";
                return false;
            }
            foreach (BlastDoor bd in UnityEngine.Object.FindObjectsOfType<BlastDoor>())
            {
                bd.SetClosed(true);
            }
            response = "Successfully closed blast doors.";
            return true;
        }
    }
}
