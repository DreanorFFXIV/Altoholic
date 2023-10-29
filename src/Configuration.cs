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
        private DalamudPluginInterface? _pluginInterface;

        public List<CharacterContainer> CharacterContainers { get; set; }

        public void Initialize(DalamudPluginInterface pluginInterface)
        {
            _pluginInterface = pluginInterface;
        }

        public void Save()
        { 
            _pluginInterface!.SavePluginConfig(this);
        }
    }
}
