﻿using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Exiled.API.Extensions;
using Interactables.Interobjects;
using MEC;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Exiled.API.Features.Roles;

namespace FacilityControl
{
    public class EventHandlers
    {
        // Server Events
        public void OnRoundStarted()
        {
            FacilityControl.EscapingDisabled = false;
            foreach (KeyValuePair<string, List<Player>> data in FacilityControl.PlySets)
            {
                FacilityControl.PlySets[data.Key].Clear();
            }

            // Credit for the original idea behind SCP lockdown goes to AlmightyLks's SCPLockdown plugin, found here: https://github.com/AlmightyLks/SCPLockdown
            /*foreach (KeyValuePair<RoleType, int> data in FacilityControl.Instance.Config.ScpLockdownPeriod)
            {
                if (data.Value == 0) continue;
                FacilityControl.ScpRoomLockdown[data.Key] = true;
                Timing.CallDelayed(data.Value, () =>
                {
                    FacilityControl.ScpRoomLockdown[data.Key] = false;
                    List<Player> PlyList = Player.List.Where(P => P.Role == data.Key).ToList();
                    foreach (Player Ply in PlyList)
                    {
                        if (data.Key == RoleType.Scp079 && Ply.Role is Scp079Role role)
                        {
                            role.MaxEnergy = 100f;
                            role.Energy = 100f;
                        }
                        else if (data.Key == RoleType.Scp106)
                        {
                            Ply.Position = RoleType.Scp106.GetRandomSpawnProperties().Item1;
                            Timing.CallDelayed(0.3f, () =>
                            {
                                Ply.ReferenceHub.scp106PlayerScript.DeletePortal();
                            });
                        }
                        if (Ply != null && FacilityControl.Instance.Config.ScpLockdownOpenNotif == true)
                        {
                            Ply.ShowHint(FacilityControl.Instance.Config.ScpLockdownOpenMessage, 2);
                        }
                    }
                });
            } */
        }

        // Player Events
        public void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (ev.Player.ReferenceHub.isDedicatedServer) return;
            if (FacilityControl.PlySets["NoInteractDoors"].Contains(ev.Player))
            {
                ev.IsAllowed = false;
                return;
            }
            if (FacilityControl.PlySets["PryGates"].Contains(ev.Player) && ev.Door.Base is PryableDoor)
            {
                ev.IsAllowed = false;
                ev.Door.TryPryOpen();
            }
            else if (FacilityControl.PlySets["DestroyDoors"].Contains(ev.Player) && ev.Door.Base is BreakableDoor)
            {
                if (ev.IsAllowed == true)
                {
                    ev.Door.BreakDoor();
                    return;
                }
            }
            /*if (FacilityControl.ScpRoomLockdown.ContainsKey(ev.Player.Role) && FacilityControl.ScpRoomLockdown[ev.Player.Role] == true)
            {
                ev.IsAllowed = false;
                if (FacilityControl.Instance.Config.ScpLockdownLockedNotif == true)
                {
                    ev.Player.ShowHint(FacilityControl.Instance.Config.ScpLockdownLockedMessage, 2);
                }
            }*/
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.NewRole == RoleType.Scp079 && ev.Player.Role is Scp079Role role && FacilityControl.ScpRoomLockdown[RoleType.Scp079] == true)
            {
                role.MaxEnergy = 100f;
                role.Energy = 100f;
            }
            else if (ev.NewRole == RoleType.Scp106 && FacilityControl.ScpRoomLockdown[RoleType.Scp106] == true)
            {
                Timing.CallDelayed(0.5f, () =>
                {
                    ev.Player.Position = new Vector3(0, -1998, 0);
                });
            }
        }

        public void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if ((FacilityControl.Instance.Config.BlackoutTeslasDisabled == true && FacilityControl.LightsOut[ZoneType.HeavyContainment] == true) || FacilityControl.TeslasDisabled)
            {
                ev.IsTriggerable = false;
            }
            else
            {
                bool canDisable = false;
                foreach (ItemType i in FacilityControl.Instance.Config.TeslaItems)
                {
                    (bool hasItem, bool isHolding) = API.GetItemInInventory(ev.Player, i);
                    if (hasItem)
                    {
                        if (FacilityControl.Instance.Config.TeslaHoldItems == true)
                        {
                            if (isHolding) canDisable = true;
                        }
                        else
                        {
                            canDisable = true;
                        }
                    }
                }
                if (canDisable)
                {
                    ev.IsTriggerable = false;
                }
            }
        }

        public void OnEscaping(EscapingEventArgs ev)
        {
            ev.IsAllowed = (!FacilityControl.EscapingDisabled);
        }

        public void OnHurting(HurtingEventArgs ev)
        {
            
        }
    }
}
