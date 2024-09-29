using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;


namespace FFOnRoundEnd {
    public class Plugin : Plugin<Config> {
        private Plugin _plugin;

        public override Version Version => new Version(1, 0, 0);
        public override string Author => "[Девопёс]Сэр Енот";
        public override string Name => "FF on round end";

        public override void OnEnabled() {
            _plugin = this;
            Exiled.Events.Handlers.Server.RoundEnded += _plugin.OnRoundEnding;
            Exiled.Events.Handlers.Server.RoundStarted += _plugin.OnRoundStars;
            
        }

        public override void OnDisabled() {
            Exiled.Events.Handlers.Server.RoundEnded -= _plugin.OnRoundEnding;
            Exiled.Events.Handlers.Server.RoundStarted -= _plugin.OnRoundStars;
            _plugin = null;
        }

        private void OnRoundEnding(RoundEndedEventArgs ev) {
            Map.Broadcast(5, "<b><color=red>Начинайте резню!!</color></b>");
            Server.FriendlyFire = true;
        }

        private void OnRoundStars() {
            Map.Broadcast(5,"<b><color=green>Резня отменяется</color></b>");
            Server.FriendlyFire = false;
        }
    }
}