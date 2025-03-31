using Exiled.API.Features;
using System;
using Exiled.Events.Handlers;
using System.Collections.Generic;


namespace RoleSwap
{
    class Plugin : Plugin<Config>
    {
        public override string Prefix => "Cactus-Patch";
        public override string Name => "RoleSwap";
        public override string Author => "Noobest1001";
        public override Version Version => new(1, 0, 0);
        public override Version RequiredExiledVersion => new(9, 5, 0);
        public static Plugin Instance;

        public EventHandlers EventHandlers { get; private set; }

        public override void OnEnabled()
        {
            Instance = this;
            EventHandlers = new EventHandlers(this);
            Exiled.Events.Handlers.Server.RoundStarted += EventHandlers.OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded += EventHandlers.OnEndingRound;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            EventHandlers = new EventHandlers(null);
            Exiled.Events.Handlers.Server.RoundStarted -= EventHandlers.OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded -= EventHandlers.OnEndingRound;
            base.OnDisabled();
        }
    }
}
