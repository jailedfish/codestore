using System.Collections.Generic;
using Exiled.API.Interfaces;
using Exiled.Permissions;

namespace Donats {
    public class Config : IConfig {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public List<List<Permissions>> Perms { get; set; } 
    }
}