using Dalamud.Configuration;
using Dalamud.Plugin;
using System;
using System.Collections.Generic;
using Altoholic.Data;

namespace Altoholic
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 3;

        [NonSerialized]
        private DalamudPluginInterface? PluginInterface;

        public List<CharacterContainer> CharacterContainers { get; set; }

        public void Initialize(DalamudPluginInterface pluginInterface)
        {
            PluginInterface = pluginInterface;
        }

        public void Save()
        { 
            PluginInterface!.SavePluginConfig(this);
        }

        public void Load()
        {
            PluginInterface!.GetPluginConfig();
        }
    }
}
