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
        private protected readonly Plugin _plugin;

        internal EventHandlers(Plugin plugin) => this._plugin = plugin;

        internal void OnRoundStarted()
        {
            Timing.RunCoroutine(ScannerRoutine());
        }

        internal void OnEndingRound() { Timing.KillCoroutines(); }

        public IEnumerator<float> ScannerRoutine()
        {
            for (; ; )
            {
                
                foreach (Player human in Player.List.Where(x => !x.IsScp))
                {
                    Log.Info($"{human.Role} is at {human.Position}");
                    if (Vector3.Distance(human.Position, Escapepos) <= 10)
                    {
                        if (human.IsNTF && human.IsCuffed)
                        {
                            human.Role.Set(Plugin.Instance.Config.NTFEscape, Enums.SpawnReason.Escaped);
                            human.Position = new(136f, 996f, -47);
                        }
                        else if (human.IsCHI && human.IsCuffed)
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
