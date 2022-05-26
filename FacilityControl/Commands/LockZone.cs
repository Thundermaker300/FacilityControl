using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandSystem;
using Exiled.Permissions.Extensions;
using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using MEC;

namespace FacilityControl.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class LockZone : ICommand
    {
        public string Command { get; set; } = "lockzone";

        public string[] Aliases { get; set; } = { "lzone", "lz" };

        public string Description { get; set; } = "Locks all the doors in the specified zone for the specified duration.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("fctrl.zones"))
            {
                response = "Access denied.";
                return false;
            }
            if (arguments.Count() < 2)
            {
                response = "Invalid format. Must be: \"closezone light/heavy/entrance duration (eg. closezone light 5)";
                return false;
            }
            if (arguments.At(0).ToLower() != "light" && arguments.At(0).ToLower() != "heavy" && arguments.At(0).ToLower() != "entrance")
            {
                response = "First argument must be light, heavy, or entrance";
                return false;
            }
            ZoneType zone = (arguments.At(0).ToLower() == "light" ? ZoneType.LightContainment : (arguments.At(0).ToLower() == "heavy" ? ZoneType.HeavyContainment : (arguments.At(0).ToLower() == "entrance" ? ZoneType.Entrance : ZoneType.Unspecified)));
            int length;
            try
            {
                length = Convert.ToInt32(arguments.At(1));
            }
            catch
            {
                response = "Second argument must be a valid number (duration)";
                return false;
            }
            foreach (Room r in Room.List)
            {
                if (r.Zone == zone)
                {
                    foreach (Door d in r.Doors)
                    {
                        d.IsOpen = false;
                        d.ChangeLock(DoorLockType.AdminCommand);
                        Timing.CallDelayed(length, () =>
                        {
                            d.ChangeLock(DoorLockType.None);
                        });
                    }
                }
            }
            response = $"Successfully locked all doors in {zone.ToString()}";
            return true;
        }
    }
}
