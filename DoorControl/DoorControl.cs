using Exiled.API.Features;
using Exiled.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Events = Exiled.Events.Handlers;

namespace DoorControl
{
    public class DoorControl : Plugin<Config>
    {
        public override string Name { get; } = "DoorControl";
        public override string Author { get; } = "Thunder";
        public override Version Version { get; } = new Version(0, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(2, 1, 6);

        public static DoorControl Instance;
        private EventHandlers handler = new EventHandlers();

        public static Dictionary<string, List<Player>> PlySets = new Dictionary<string, List<Player>>
        {
            ["NoInteractDoors"] = new List<Player> { },
            ["DestroyDoors"] = new List<Player> { },
        };

        public override void OnEnabled()
        {
            base.OnEnabled();

            if (!this.Config.IsEnabled) return;

            Instance = this;

            // Server Events
            Events.Server.RoundStarted += handler.OnRoundStarted;

            // Player Events
            Events.Player.InteractingDoor += handler.OnInteractingDoor;
        }

        public override void OnDisabled()
        {
            base.OnDisabled();

            if (!this.Config.IsEnabled) return;
        }
    }
}
