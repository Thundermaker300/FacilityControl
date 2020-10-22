using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Events = Exiled.Events.Handlers;

namespace FacilityControl
{
    public class FacilityControl : Plugin<Config>
    {
        public override string Name { get; } = "FacilityControl";
        public override string Author { get; } = "Thunder";
        public override Version Version { get; } = new Version(0, 1, 1);
        public override Version RequiredExiledVersion { get; } = new Version(2, 1, 6);

        public static FacilityControl Instance;
        private EventHandlers handler = new EventHandlers();

        public int patchesCounter { get; private set; }
        public Harmony harmony { get; private set; }

        public static Dictionary<string, List<Player>> PlySets = new Dictionary<string, List<Player>>
        {
            ["NoInteractDoors"] = new List<Player> { },
            ["DestroyDoors"] = new List<Player> { },
            ["PryGates"] = new List<Player> { },
        };

        public static Dictionary<ZoneType, bool> LightsOut = new Dictionary<ZoneType, bool>
        {
            [ZoneType.LightContainment] = false,
            [ZoneType.HeavyContainment] = false,
            [ZoneType.Entrance] = false,
        };

        public static Dictionary<RoleType, bool> ScpRoomLockdown = new Dictionary<RoleType, bool>
        {
            [RoleType.Scp049] = false,
            [RoleType.Scp0492] = false,
            [RoleType.Scp079] = false,
            [RoleType.Scp096] = false,
            [RoleType.Scp106] = false,
            [RoleType.Scp173] = false,
            [RoleType.Scp93953] = false,
            [RoleType.Scp93989] = false,
        };

        public static bool EscapingDisabled = false;
        public static bool TeslasDisabled = false;

        public override void OnEnabled()
        {
            base.OnEnabled();

            if (!this.Config.IsEnabled) return;

            Instance = this;

            // Server Events
            Events.Server.RoundStarted += handler.OnRoundStarted;

            // Player Events
            Events.Player.InteractingDoor += handler.OnInteractingDoor;
            Events.Player.ChangingRole += handler.OnChangingRole;
            Events.Player.TriggeringTesla += handler.OnTriggeringTesla;
            Events.Player.Escaping += handler.OnEscaping;
            Events.Player.Hurting += handler.OnHurting;

            // Harmony
            try
            {
                harmony = new Harmony($"FacilityControl-{++patchesCounter}");
                harmony.PatchAll();
                Log.Info("FacilityControl patching success.");
            }
            catch (Exception e)
            {
                Log.Error($"Patching failed! {e}");
            }
        }
        

        public override void OnDisabled()
        {
            base.OnDisabled();

            if (!this.Config.IsEnabled) return;

            // Server Events
            Events.Server.RoundStarted -= handler.OnRoundStarted;

            // Player Events
            Events.Player.InteractingDoor -= handler.OnInteractingDoor;
            Events.Player.TriggeringTesla -= handler.OnTriggeringTesla;
            Events.Player.Escaping -= handler.OnEscaping;
            Events.Player.Hurting -= handler.OnHurting;

            // Harmony
            try
            {
                harmony.UnpatchAll();
            }
            catch { }

            Instance = null;
        }
    }
}
