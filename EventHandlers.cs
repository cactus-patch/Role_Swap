#pragma warning disable CS8602 // Dereference of a possibly null reference.

using Exiled.API.Features;
using MEC;
using PlayerRoles;
using UnityEngine;
using Exiled.Events.EventArgs.Server;
using Exiled.API.Enums;

namespace RoleSwap
{
    class EventHandlers(Plugin plugin)
    {
        public Vector3 Escapepos = new(123f, 988f, 21f);
        public Plugin? plugin = plugin;
        private readonly CoroutineHandle scanningCoroutine;

        public void OnRoundStarted()
        {
            Timing.RunCoroutine(ScannerRoutine());   
        }

#pragma warning disable IDE0060 // Remove unused parameter
        public void OnEndingRound(EndingRoundEventArgs ev)
#pragma warning restore IDE0060 // Remove unused parameter
        { 
            Timing.KillCoroutines(scanningCoroutine); 
        }

        public IEnumerator<float> ScannerRoutine()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(.5f);

                if(Round.IsEnded) break;

                foreach(Player human in Player.List.Where(x => !x.IsScp))
                {
                    if (Vector3.Distance(human.Position, Escapepos) <= Plugin.Instance.Config.EscapeDistance)
                    {
                        if(human.Role.Team == Team.FoundationForces && human.IsCuffed)
                        {
                            human.Role.Set(Plugin.Instance.Config.FFEscape, SpawnReason.Escaped);
                            human.Position = new(7f, 992f, -42);
                            Respawn.GrantTokens(Faction.FoundationEnemy, 5);
                            Respawn.AdvanceTimer(Faction.FoundationEnemy, 20);
                        }
                        else if(human.IsCHI && human.IsCuffed)
                        {
                            human.Role.Set(Plugin.Instance.Config.CIEscape, SpawnReason.Escaped);
                            human.Position = new(136f, 996f, -47);
                            Respawn.GrantTokens(Faction.FoundationStaff, 5);
                            Respawn.AdvanceTimer(Faction.FoundationStaff, 20);
                        }
                    }
                }
            }
        }
    }
}
#pragma warning restore CS8602 // Dereference of a possibly null reference.