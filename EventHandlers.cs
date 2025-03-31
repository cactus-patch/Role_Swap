using System;
using System.Collections.Generic;
using System.Linq;

using Exiled.API.Features;
using Enums = Exiled.API.Enums;
using MEC;
using PlayerRoles;
using UnityEngine;
using Metrics;
using Exiled.Events.EventArgs.Server;

namespace RoleSwap
{
    class EventHandlers
    {
        public Vector3 Escapepos = new(123f, 988f, 21f);
        public Plugin plugin;

        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        internal void OnRoundStarted()
        {
            Timing.RunCoroutine(ScannerRoutine());
        }

        internal void OnEndingRound() { Timing.KillCoroutines(); }

        public IEnumerator<float> ScannerRoutine()
        {
            while(true)
            {
                yield return Timing.WaitForSeconds(.5f);

                if(Round.isEnding) break;

                foreach(Player human in Player.List.Where(x => !x.IsSCP && !x.IsNPC))
                {
                    if(plugin.Instance.Config.Debug) 
                    {
                        Log.Info($"{player.Name} is at {player.Position} (Zone: {player.Zone})")
                    }
                    if(Vector3.Distance(human.Position, Escapepos) <= 10)
                    {
                        if(human.isNTF && human.isCuffed)
                        {
                            human.Role.Set(Plugin.Instance.Config.NTFEscape, Enums.SpawnReason.Escaped);
                            human.Position = new(136f, 996f, -47);
                        }
                        else if(human.IsCHI && human.IsCuffed)
                        {
                            human.Role.Set(Plugin.Instance.Config.CIEscape, Enums.SpawnReason.Escaped);
                            human.Position = new(7f, 992f, -42);
                        }
                    }
                }
            }
        }
    }
}
