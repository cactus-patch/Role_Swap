using System.Collections.Generic;
using System.Linq;
using Exiled.Events.EventArgs;
using Exiled.API.Features;
using Enums = Exiled.API.Enums;
using MEC;
using PlayerRoles;
using UnityEngine;
using Exiled.Events.EventArgs.Server;
using Exiled.Events.EventArgs.Player;

namespace RoleSwap
{
    class EventHandlers
    {
        public Vector3 Escapepos = new(123f, 988f, 21f);
        public Plugin plugin;
        private CoroutineHandle scanningCoroutine;

        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        public void OnRoundStarted()
        {
            Timing.RunCoroutine(ScannerRoutine());   
        }

        public void OnEndingRound(EndingRoundEventArgs ev) 
        { 
            Timing.KillCoroutines(scanningCoroutine); 
        }

        public override void OnCuffed(CuffedEventArgs ev)
        {
            ev.Target.IsGodModeEnabled = true;
        }

        public override void OnUncuffed(RemovedCuffedEventArgs ev)
        {
            ev.Target.IsGodModeEnabled = false;
        }

        public IEnumerator<float> ScannerRoutine()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(.5f);

                if(Round.IsEnded) break;

                foreach(Player human in Player.List.Where(x => !x.IsScp))
                {
                    if(Plugin.Instance.Config.Debug) 
                    {
                        Log.Info($"{human.DisplayNickname} is at {human.Position} (Zone: {human.Zone})");
                    }
                    if(Vector3.Distance(human.Position, Escapepos) <= Plugin.Instance.Config.EscapeDistance)
                    {
                        if(human.Role.Team == Team.FoundationForces && human.IsCuffed)
                        {
                            human.Role.Set(Plugin.Instance.Config.FFEscape, Enums.SpawnReason.Escaped);
                            human.Position = new(7f, 992f, -42);
                        }
                        else if(human.IsCHI && human.IsCuffed)
                        {
                            human.Role.Set(Plugin.Instance.Config.CIEscape, Enums.SpawnReason.Escaped);
                            human.Position = new(136f, 996f, -47);
                        }
                    }
                }
            }
        }
    }
}
