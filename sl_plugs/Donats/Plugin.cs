using Exiled.API.Features;

namespace Donats {
    public class Plugin : Plugin<Config> {
        private Plugin plugin;

        public override void OnEnabled() {
            plugin = this;

            base.OnEnabled();
        }

        public override void OnDisabled() {
            
            plugin = null;
            base.OnDisabled();
        }
    }
}