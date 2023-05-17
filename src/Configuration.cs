using Dalamud.Configuration;
using Dalamud.Plugin;
using System;
using System.Collections.Generic;

namespace Altoholic
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 0;

        [NonSerialized]
        private DalamudPluginInterface? PluginInterface;

        public List<CharacterData> CharacterData { get; set; }

        public void Initialize(DalamudPluginInterface pluginInterface)
        {
            this.PluginInterface = pluginInterface;
        }

        public void Save()
        { 
            this.PluginInterface!.SavePluginConfig(this);
        }

        public void Load()
        {
            this.PluginInterface!.GetPluginConfig();
        }
    }
}
