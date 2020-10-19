using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;
using Mirror;

namespace FacilityControl.Patches
{
    [HarmonyPatch(typeof(Escape), nameof(Escape.TargetShowEscapeMessage))]
    class EscapePatch
    {
        public static bool Prefix()
        {
            return (!FacilityControl.EscapingDisabled);
        }
    }
}
