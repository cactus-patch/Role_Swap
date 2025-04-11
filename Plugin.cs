using Server = Exiled.Events.Handlers.Server;
using Player = Exiled.Events.EventArgs.Player;


namespace RoleSwap
{
    class Plugin : Plugin<Config>
    {
        public override string Prefix => "Cactus-Patch";
        public override string Name => "Arrested Development";
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
            Server.EndingRound += eventHandlers.OnEndingRound;
            Player.Handcuffing += eventHandlers.OnCuffed;
            Player.RemovedHandcuffs += eventHandlers.OnUncuffed;


            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Server.RoundStarted -= eventHandlers.OnRoundStarted;
            Server.EndingRound -= eventHandlers.OnEndingRound;
            Player.Handcuffing -= eventHandlers.OnCuffed;
            Player.RemovedHandcuffs -= eventHandlers.OnUncuffed;

            Instance = null;
            eventHandlers = null;
            base.OnDisabled();
        }
    }
}
