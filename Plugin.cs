using Exiled.API.Features;
using System;
using Exiled.Events.Handlers;
using System.Collections.Generic;
using Server = Exiled.Events.Handlers.Server;


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

        private EventHandlers eventHandlers;

        public override void OnEnabled()
        {
            Instance = this;
            eventHandlers = new EventHandlers(this);

            Server.RoundStarted += eventHandlers.OnRoundStarted;
            Server.RoundEnded += eventHandlers.OnEndingRound;
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            eventHandlers = null;
            Server.RoundStarted -= eventHandlers.OnRoundStarted;
            Server.RoundEnded -= eventHandlers.OnEndingRound;
            base.OnDisabled();
        }
    }
}
