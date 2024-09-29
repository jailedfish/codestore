using Exiled.API.Interfaces;

namespace CoinUsage {
    public class Config : IConfig {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public bool ShowIntro { get; set; } = true;
    }
}