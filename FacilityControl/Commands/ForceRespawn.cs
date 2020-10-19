using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommandSystem;
using Exiled.Permissions.Extensions;
using Exiled.API.Enums;
using Exiled.API.Features;
using Respawning;
using MEC;

namespace FacilityControl.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class ForceRespawn : ICommand
    {
        public string Command { get; set; } = "forcerespawn";

        public string[] Aliases { get; set; } = { "fre" };

        public string Description { get; set; } = "Same as the built in respawn command, however this command also spawns vehicles and waits for them to get to the proper position before spawning.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("fctrl.respawn"))
            {
                response = "Access denied.";
                return false;
            }
            if (arguments.Count() < 1)
            {
                response = "Proper usage: \"forcerespawn (mtf/chaos)\"";
                return false;
            }
            if (arguments.At(0).ToLower() != "mtf" && arguments.At(0).ToLower() != "chaos")
            {
                response = "Provided argument must be \"mtf\" or \"chaos\".";
                return false;
            }
            SpawnableTeamType spawningTeam = (arguments.At(0).ToLower() == "mtf" ? SpawnableTeamType.NineTailedFox : SpawnableTeamType.ChaosInsurgency);
            float length = (spawningTeam == SpawnableTeamType.NineTailedFox ? 18f : 13f);
            RespawnManager.Singleton.RestartSequence(); // Prevent the opposite team from spawning while the forced team's animation is playing.
            RespawnEffectsController.ExecuteAllEffects(RespawnEffectsController.EffectType.Selection, spawningTeam);
            Timing.CallDelayed(length, () =>
            {
                RespawnManager.Singleton.ForceSpawnTeam(spawningTeam);
            });
            response = $"Forcing {spawningTeam} spawn...";
            return true;
        }
    }
}
