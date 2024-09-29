using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using Player = Exiled.API.Features.Player;

namespace ScpObjectRole {
    public class Plugin : Plugin<Config> {
        public override Version Version => new Version(1, 0, 0);
        public override string Author => "[Девопёс]Сэр Енот";
        public override string Name => "Object scp role";
        
        public override void OnEnabled() {
            Exiled.Events.Handlers.Player.Spawned += OnPlayerSpawn;
            base.OnEnabled();
        }

        public override void OnDisabled() {
            Exiled.Events.Handlers.Player.Spawned -= OnPlayerSpawn;
            
            base.OnDisabled();
        }

        private void OnPlayerSpawn(SpawnedEventArgs ev) {
            if ((ev.Reason == SpawnReason.RoundStart || ev.Reason == SpawnReason.LateJoin || (ev.Reason == SpawnReason.ForceClass && Config.Debug)) &&
                Player.List.Count(plr => plr.Role == ev.Player.Role) >= Config.MinPlayers) {
                ev.Player.ShowHint("You can swap as scp objects like scp-500, scp-268 etc.");
                ev.Player.CustomInfo = Config.PendingInfo;
            }
        }
    }
    
    [CommandHandler(typeof(ClientCommandHandler))]
    public class MakeMe : ICommand {
        private readonly Config _config = new Config();
        
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response) {
            response = "";
            if (arguments.Count() != 1) {
                response = "Usage: .swap_to scp-<scp_number>\nExamples: .swap_to scp-500, .swap_to scp-[REDACTED]";
                return false;
            }
            Log.Info(0);
            Player player = Player.Get(sender);

            if (player == null) {
                response = "Error";
                return false;
            }
            
            Log.Info(1);
            if (player.CustomInfo != null) {
                response = "You can't swap (maybe you respawned or force classed)";
                return false;
            }
            
            Log.Info(2);
            if (Round.ElapsedTime.TotalSeconds >= _config.TimeOut) {
                response = "Role change time ended";
                return false;
            }
            
            Log.Info(3);
            if (Player.List.Count((plr) => plr.CustomInfo.StartsWith("SCP_OBJ")) >= _config.MaxObjs) {
                response = "It's max SCPs on server try in next round";
                return false;
            }
            
            Log.Info(4);
            if (Player.List.Any((plr) => plr.CustomInfo.EndsWith(arguments.ToArray()[0]))) {
                response = "It's other this scp";
                return false;
            }
            
            Log.Info(5);
            player?.Role.Set(RoleTypeId.Tutorial, SpawnReason.RoundStart);
            player?.EnableEffect(EffectType.Invisible);
            Log.Info(6);
            Timing.RunCoroutine(_objectMoving(Item.Create(ItemType.SCP500), player));
            
            return true;
        }

        public string Command => "make_me";
        public string[] Aliases { get; } = { "swap_to", "to_scp" };
        public string Description => "";

        private IEnumerator<float> _objectMoving(Item obj, Player player) {
            List<Pickup> objs = new List<Pickup>
            {
                [0] = obj.CreatePickup(player.Position),
                [1] = obj.CreatePickup(player.Position)
            };

            while (player.IsAlive) {
                
                objs[0].Destroy();
                objs[0] = objs[1];
                objs[1] = obj.CreatePickup(player.Position);
                
                yield return Timing.WaitForOneFrame;
            }
        }
    }
    
}