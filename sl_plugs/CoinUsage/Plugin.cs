using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.Events.EventArgs.Player;
using UnityEngine.Windows;
using Player = Exiled.Events.Handlers.Player;


//using Player = Exiled.Events.Handlers.Player;

namespace CoinUsage {
    public class Plugin : Plugin<Config> {
        private Plugin plugin;
        
        public override Version Version => new Version(1, 0, 0);
        public override string Author => "[Девопёс]Сэр Енот";
        public override string Name => "Coin Teleport";

        public override void OnEnabled() {
            plugin = this;
            
            Player.FlippingCoin += plugin.OnCoinFlip;
            if (Config.ShowIntro) {
                Player.Spawned += plugin.Introdution;
            }

            base.OnEnabled();
        }

        public override void OnDisabled() {
            Player.FlippingCoin -= plugin.OnCoinFlip;
            if (Config.ShowIntro) {
                Player.Spawned -= plugin.Introdution;
            }

            plugin = null;
            base.OnDisabled();
        }

        void OnCoinFlip(FlippingCoinEventArgs ev) {
            ev.Player.Teleport(Door.List.Where((door) => door.Zone == ev.Player.Zone && !door.IsGate).GetRandomValue());
            ev.Player.RemoveItem(ev.Player.Items.GetRandomValue());
            ev.Player.Broadcast(5, "<b>You've flipped coin good luck<b>");
        }

        void Introdution(SpawnedEventArgs ev) {
            if (ev.Reason == SpawnReason.RoundStart | ev.Reason == SpawnReason.LateJoin) {
                ev.Player.Broadcast(5, "Pick up some coins, there are reasons for this");
            }
            else if (ev.Reason == SpawnReason.Escaped) {
                ev.Player.Broadcast(4, "Do not forgot about coins!");
            }
        }
    }
}