/*
using Exiled.API.Features;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacilityControl.Patches
{
    [HarmonyPatch(typeof(Scp079PlayerScript), nameof(Scp079PlayerScript.ServerUpdateMana))]
    class Scp079LockdownPatch
    {
        public static bool Prefix(Scp079PlayerScript __instance)
        {
            return !FacilityControl.ScpRoomLockdown[RoleType.Scp079];
        }
    }
}
*/