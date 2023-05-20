using System;
using System.Collections.Generic;
using System.Linq;
using Altoholic.Data;
using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using Altoholic.Windows;
using Dalamud.Data;
using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using Lumina.Excel.GeneratedSheets;

namespace Altoholic
{
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "Altoholic";
        private const string CommandName = "/alt";

        private DalamudPluginInterface PluginInterface { get; }
        private ClientState ClientState { get; }
        private CommandManager CommandManager { get; }
        private DataManager Dm { get; }
        private Configuration Configuration { get; }
        private WindowSystem WindowSystem = new("Altoholic");
        private MainWindow MainWindow { get; }

        public Plugin(
            [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
            [RequiredVersion("1.0")] CommandManager commandManager,
            [RequiredVersion("1.0")] ClientState clientState,
            [RequiredVersion("1.0")] DataManager dm)
        {
            PluginInterface = pluginInterface;
            CommandManager = commandManager;
            Dm = dm;
            ClientState = clientState;

            Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            Configuration.Initialize(PluginInterface);
            
            //reset old versions 
            if (Configuration.Version != 3)
            {
                Configuration.CharacterContainers = new List<CharacterContainer>();
                Configuration.Version = 3;
                Configuration.Save();
            }
            
            CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
            {
                HelpMessage = "Opens the Altoholic View"
            });
            
            MainWindow = new MainWindow(commandManager);
            WindowSystem.AddWindow(MainWindow);

            PluginInterface.UiBuilder.Draw += DrawUI;
        }

        public void Dispose()
        {
            WindowSystem.RemoveAllWindows();
            
            MainWindow.Dispose();
            
            CommandManager.RemoveHandler(CommandName);
        }

        private void OnCommand(string command, string args)
        {
            MainWindow.CharacterContainers = LoadData();
            MainWindow.IsOpen = true;
        }
 
        private void DrawUI()
        {
            WindowSystem.Draw();
        }

        private List<CharacterContainer> LoadData()
        {
            Configuration.Load();

            var characterContainers = Configuration.CharacterContainers ?? new List<CharacterContainer>(); //null on new install
            if (ClientState.LocalPlayer != null)
            {
                var loggedCharacter = $"{ClientState.LocalPlayer.Name.ToString()}@{ClientState.LocalPlayer.HomeWorld.GameData.Name}";

                var existingCharacter = characterContainers.FirstOrDefault(x => x.Name == loggedCharacter);
                if (existingCharacter != null)
                {
                    existingCharacter.Reload(Dm); 
                }
                else 
                {  
                    characterContainers.Add(new CharacterContainer(loggedCharacter, Dm));
                }
            }

            Configuration.CharacterContainers = characterContainers;
            Configuration.Save();
            return characterContainers;
        }
    }
}
