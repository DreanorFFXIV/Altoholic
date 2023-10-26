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
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using Lumina.Excel.GeneratedSheets;

namespace Altoholic
{
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "Altoholic";
        private const string CommandName = "/alt";

        private DalamudPluginInterface PluginInterface { get; }
        private IClientState ClientState { get; }
        private ICommandManager CommandManager { get; }
        private IDataManager Dm { get; }
        private Configuration Configuration { get; }
        private WindowSystem WindowSystem = new("Altoholic");
        private MainWindow MainWindow { get; }

        public Plugin(
            [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
            [RequiredVersion("1.0")] ICommandManager commandManager,
            [RequiredVersion("1.0")] IClientState clientState,
            [RequiredVersion("1.0")] IDataManager dm)
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
            //Configuration.Save();
            WindowSystem.RemoveAllWindows();
            
            MainWindow.Dispose();
            
            CommandManager.RemoveHandler(CommandName);
        }

        private void OnCommand(string command, string args)
        {
            MainWindow.CharacterContainers = LoadData();
          //  MainWindow.IsOpen = true;
        }
 
        private void DrawUI()
        {
            WindowSystem.Draw();
        }

        private List<CharacterContainer> LoadData()
        {
            PluginLog.Log("LoadData()");
            Configuration.Load();

            var characterContainers = Configuration.CharacterContainers ?? new List<CharacterContainer>(); //null on new install
           /*
            if (ClientState.LocalPlayer != null)
            {
                var loggedCharacter = $"{ClientState.LocalPlayer.Name.ToString()}@{ClientState.LocalPlayer.HomeWorld.GameData.Name}";

                var existingCharacter = characterContainers.FirstOrDefault(x => x.Name == loggedCharacter);
                if (existingCharacter != null)
                {
                    PluginLog.Log("Exist: " + existingCharacter.Name);
                    existingCharacter.Reload(Dm); 
                }
                else 
                {  
                    PluginLog.Log("Add:"+ loggedCharacter);
                    characterContainers.Add(new CharacterContainer(loggedCharacter, Dm));
                }
            }
*/
           foreach (var a in characterContainers)
           {
               PluginLog.Log(a.Currency.Battle.Poetics);
           }
            Configuration.CharacterContainers = characterContainers;
            //Configuration.Save();
            return characterContainers;
        }
    }
}
