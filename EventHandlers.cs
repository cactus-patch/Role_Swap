#pragma warning disable CS8602 // Dereference of a possibly null reference.

using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;
using MEC;
using PlayerRoles;
using UnityEngine;

namespace RoleSwap
{
    class EventHandlers(Plugin plugin)
    {
        public Vector3 Escapepos = new(123f, 288f, 21f);
        public Plugin? plugin = plugin;
        private readonly CoroutineHandle scanningCoroutine;
        private readonly CoroutineHandle antiHatGlitchCoroutine;

        public void OnRoundStarted()
        {
            Timing.RunCoroutine(ScannerRoutine());
            Timing.RunCoroutine(AntiHatGlitch());
        }

#pragma warning disable IDE0060 // Remove unused parameter
        public void OnEndingRound(EndingRoundEventArgs ev)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            Timing.KillCoroutines(scanningCoroutine);
            Timing.KillCoroutines(antiHatGlitchCoroutine);
        }

        public IEnumerator<float> ScannerRoutine()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(.5f);

                if (Round.IsEnded) break;
                var tokensCHI = Respawn.GetInfluence(Faction.FoundationEnemy);
                var tokensNTF = Respawn.GetInfluence(Faction.FoundationStaff);

                foreach (Player human in Player.List.Where(x => !x.IsScp))
                {
                    if (Vector3.Distance(human.Position, Escapepos) <= Plugin.Instance.Config.EscapeDistance)
                    {
                        if (human.Role.Team == Team.FoundationForces && human.IsCuffed)
                        {
                            human.Role.Set(Plugin.Instance.Config.FFEscape, SpawnReason.Escaped);
                            human.Position = new(7f, 292f, -42);
                            Respawn.GrantInfluence(Faction.FoundationEnemy, 5);
                            Respawn.AdvanceTimer(Faction.FoundationEnemy, 20);
                        }
                        else if (human.IsCHI && human.IsCuffed)
                        {
                            human.Role.Set(Plugin.Instance.Config.CIEscape, SpawnReason.Escaped);
                            human.Position = new(136f, 296f, -47);
                            Respawn.GrantInfluence(Faction.FoundationStaff, 5);
                            Respawn.AdvanceTimer(Faction.FoundationStaff, 20);
                        }
                    }
                }
            }
        }

        public IEnumerator<float> AntiHatGlitch()
        {
            while (!Round.IsEnded)
            {
                yield return Timing.WaitForSeconds(1f);
                foreach (Player diddied in Player.List.Where(x => x.IsInPocketDimension && x.IsAlive))
                {
                    if (!diddied.IsEffectActive<PocketCorroding>())
                    {
                        diddied.EnableEffect(EffectType.PocketCorroding);
                    }
                    continue;
                }
            }
        }
    }
}
#pragma warning restore CS8602 // Dereference of a possibly null reference.