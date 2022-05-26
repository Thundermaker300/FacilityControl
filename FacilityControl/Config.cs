using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityControl
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("If set to true, tesla gates will be disabled if the lights in HCZ are out.")]
        public bool BlackoutTeslasDisabled { get; set; } = true;

        [Description("Determines what items a player can have in their inventory in order to disable tesla gates.")]
        public List<ItemType> TeslaItems { get; set; } = new List<ItemType> { };

        [Description("If set to true, users must be holding one of the aforementioned items in order to disable tesla gates.")]
        public bool TeslaHoldItems { get; set; } = true;

        /*[Description("Determines how long SCPs will be locked in their containment chamber before they are allowed to leave. Set to 0 to disable.")]
        public Dictionary<RoleType, int> ScpLockdownPeriod { get; set; } = new Dictionary<RoleType, int>
        {
            [RoleType.Scp049] = 0,
            [RoleType.Scp0492] = 0,
            [RoleType.Scp079] = 0,
            [RoleType.Scp096] = 0,
            [RoleType.Scp106] = 0,
            [RoleType.Scp173] = 0,
            [RoleType.Scp93953] = 0,
            [RoleType.Scp93989] = 0,
        };

        [Description("Display a hint when SCPs are able to leave their containment chamber?")]
        public bool ScpLockdownOpenNotif { get; set; } = true;

        [Description("The message to show when SCPs are able to leave their rooms.")]
        public string ScpLockdownOpenMessage { get; set; } = "You can leave your containment chamber now!";

        [Description("Display a hint when SCPs attempt to leave their chamber during the lockdown period?")]
        public bool ScpLockdownLockedNotif { get; set; } = true;

        [Description("The message to show when SCPs are able to leave their rooms.")]
        public string ScpLockdownLockedMessage { get; set; } = "You cannot leave your containment chamber yet!";
        */
    }
}
