using Exiled.API.Interfaces;

namespace ScpObjectRole {
    public class Config : IConfig {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public string PendingInfo { get; set; } = "PENDING_SCP_OBJ_";
        public int MinPlayers { get; set; } = 2;
        public int TimeOut { get; set; } = 10;
        public int MaxObjs { get; set; } = 3;
    }
}