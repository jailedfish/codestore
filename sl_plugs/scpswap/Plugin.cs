using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using RemoteAdmin;
using MEC;

namespace ScpSwap {
    public class SwapRequests {
        public Dictionary<Player, Player> pending { get; } = new Dictionary<Player, Player>();  //(Key)To -> (Value)From

        public bool HavePendingSwap(Player player) {
            return pending.ContainsKey(player);
        }

        public bool PendedSwap(Player player) {
            return pending.ContainsValue(player);
        }

        public void DeletePendingSwap(Player player) {
            if (HavePendingSwap(player)) {
                pending.Remove(player);
            }
        }

        public Player GetSwapAuthor(Player player) {
            return pending[player];
        }
        public void NewSwap(Player to, Player from) {
            pending.Add(to, from);
        }
    }
    public class Plugin : Plugin<Config> {
        private Plugin plugin;
        public SwapRequests swaps;
        
        public override Version Version => new Version(0, 5, 0);
        public override string Author => "[Девопёс]Сэр Енот";
        public override string Name => "SCP Swap";

        public override void OnEnabled() {
            plugin = this;
            swaps = new SwapRequests();
            Exiled.Events.Handlers.Player.Spawned += plugin.OnPlayerSpawn; 
            
            base.OnEnabled();
        }

        public override void OnDisabled() {
            Exiled.Events.Handlers.Player.Spawned -= plugin.OnPlayerSpawn;
            
            plugin = null;
            swaps = null;
            base.OnDisabled();
        }

        void OnPlayerSpawn(SpawnedEventArgs ev) {
            if (ev.Reason == SpawnReason.RoundStart && ev.Player.Role.Side == Side.Scp | (Config.Debug && ev.Player.Role.Side == Side.Scp)) { // for debugging
                ev.Player.Broadcast(20, "This server has ScpSwap plugin\nType .swap <scp_number> to swap scp");
            }

            if (ev.Reason == SpawnReason.Died || ev.Reason == SpawnReason.Destroyed) {
                swaps.DeletePendingSwap(ev.Player);
            }
        }
    }
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Swap : ICommand {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response) {
            response = "Swap success";
            string scpRequest;
            PlayerCommandSender psender = (PlayerCommandSender)sender;
            Player player;
            
            Dictionary<string, RoleTypeId> roles = new Dictionary<string, RoleTypeId>()
            {
                {"173", RoleTypeId.Scp173},
                {"049", RoleTypeId.Scp049},
                {"079", RoleTypeId.Scp079},
                {"106", RoleTypeId.Scp106},
                {"939", RoleTypeId.Scp939},
                {"096", RoleTypeId.Scp096}
            };
            if (true) {
                if ((long)Round.ElapsedTime.TotalSeconds > 40) {
                    response = "<color=red>Swap time ended</color>";
                    return false;
                }

                if (arguments.Count != 1) {
                    response = "<color=red>Usage: .scpswap <scp_number>\nExample: .scpswap 173</color>";
                    return false;
                }

                if (roles.Keys.All((role) => role != arguments.ToArray()[0].Replace(" ", ""))) {
                    response = "<color=red>Available SCPs: 173, 049, 079, 096, 106, 939</color>";
                    return false;
                }

                if (Player.Get(psender.PlayerId).Role.Side != Side.Scp) {
                    response = "<color=red>You aren't SCP</color>";
                    return false;
                }
            }

            player = Player.Get(psender.PlayerId);
            scpRequest = arguments.ToArray()[0].Replace(" ", "");
            
            if (Player.List.Any((plr) => plr.Role == roles[scpRequest])) {
                Player.List
                    .Where((plr) => plr.Role == roles[scpRequest])
                    .ToArray()[0]
                    .Broadcast(5, $"<color=yellow>You got swap request by: <b>{psender.Nickname}{player.Role}<b></color>\n" +
                                  $"<color=green><b>To accept type .sw_accept</b></color>");
                //plugin.swaps.NewSwap(Player.List.Where((plr) => plr.Role == roles[scpRequest]).ToArray()[0], player);
            }
            else {
                player.Role.Set(roles[scpRequest]);
                player.Broadcast(5, "<color=green>Swap success</color>");
            }
            
            return true;
        }
        
        //Interface realisation
        public string Command => "scpswap";
        public string[] Aliases => new string[]
        {
            "swap",
            "change_scp",
            "scp_swap",
            "scp-swap"
        };

        public string Description => "Swap you as scp\nAvailable SCPs: 173, 049, 079, 049-2, 096, 106, 939";        
    }
    
    [CommandHandler(typeof(ClientCommandHandler))]
    public class SwapAccept : ICommand {
        public Player player;
        private Config _config = new Config();

        private SwapRequests swaps = new SwapRequests();
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response) {
            response = "Swap success";
            
            player = Player.Get(sender);
            Role oldRole = player.Role;
            if (arguments.Count > 0) {
                if (arguments.ToArray()[0] == "ExAmPlE") {
                    Log.Info(_config);
                }
            }
            if (!swaps.HavePendingSwap(player)) {
                response = "You haven't pending swaps";
                return false;
            }

            if (player.Role.Side != Side.Scp) {
                response = "You aren't scp";
                return false;
            }
            
            player.Role.Set(swaps.GetSwapAuthor(player).Role);
            swaps.GetSwapAuthor(player).Role.Set(oldRole);
            swaps.DeletePendingSwap(player);
            return true;
        }

        public string Command => "accept";
        public string[] Aliases => new string[] {
            "y",
            "do_swap",
            "yes"
        };

        public string Description => "Accept swap request";
    }
}