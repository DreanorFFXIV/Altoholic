using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Altoholic.Data;
using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using Altoholic.Windows;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;

namespace Altoholic
{
/*
            ⠀⠀⠀⠀⣀⣤⣴⣶⣶⣾⣿⣷⣶⣶⣦⣄⡀⠀⠀⠀ 
            ⠀⢠⣴⣿⣿⣿⣿⣿⣭⣭⣭⣭⣭⣿⣿⣿⣿⣧⣀⠀ 
            ⢰⣿⣿⣿⣿⣿⣯⣿⡶⠶⠶⠶⠶⣶⣭⣽⣿⣿⣷⣆ 
            ⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿ 
            ⠈⢿⣿⣿⡿⠋⠉⠁⠈⠉⠛⠉⠀⠀⠀⠈⠻⣿⡿⠃ 
            ⠀⠀⠀⠉⠁⠀⢴⣐⢦⠀⠀⠀⣴⡖⣦⠀⠀⠈⠀⠀ 
            ⠀⠀⠀⠀⠀⠀⠈⠛⠋⠀⠀⠀⠈⠛⠁⠀⠀⠀⠀⠀ 
            ⠀⠀⠀⠀⠀⣀⡀⠀⠀⠀⣀⠀⠀⠀⢀⡀⠀⠀⠀⠀ 
            ⠀⢀⡔⣻⣭⡇⠀⣼⣿⣿⣿⡇⠦⣬⣟⢓⡄⠀⠀ 
            ⠀⠀⠀⠉⠁⠀⠀⠀⣿⣿⣿⣿⡇⠀⠀⠉⠉⠀⠀⠀ 
            ⠀⠀⠀⠀⠀⠀⠀⠀⠻⠿⠿⠟⠁⠀⠀⠀⠀⠀⠀⠀ 
            ⠀⠀⠀⠀⠀⠀⣠⢼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⡄⠀⠀⠀ 
            ⠀⠀⣀⣤⣴⣾⣿⣷⣭⣭⣭⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⡀⠀⠀ 
            ⠀⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣸⣿⣿⣧⠀⠀ 
            ⠀⣿⣿⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣯⢻⣿⣿⡄⠀ 
            ⠀⢸⣿⣮⣿⣿⣿⣿⣿⣿⣿⡟⢹⣿⣿⣿⡟⢛⢻⣷⢻⣿⣧⠀ 
            ⠀⠀⣿⡏⣿⡟⡛⢻⣿⣿⣿⣿⠸⣿⣿⣿⣷⣬⣼⣿⢸⣿⣿⠀
*/
    
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "Altoholic";
        private const string CommandName = "/palt";

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
       
            CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
            {
                HelpMessage = "Opens the Altoholic View"
            });
            
            MainWindow = new MainWindow(commandManager, $"Altoholic {Assembly.GetExecutingAssembly().GetName().Version?.ToString()}");
            WindowSystem.AddWindow(MainWindow);

            clientState.Login += RefreshData;
            clientState.Logout += RefreshData;
            
            PluginInterface.UiBuilder.Draw += DrawUI;
        }

        public void Dispose()
        {
            Configuration.Save();
            WindowSystem.RemoveAllWindows();
            
            MainWindow.Dispose();
            
            CommandManager.RemoveHandler(CommandName);
        }

        private void OnCommand(string command, string args)
        {
            RefreshData();
            MainWindow.IsOpen = true;
        }

        private void RefreshData()
        {
            MainWindow.CharacterContainers = LoadData();
        }

        private void DrawUI()
        {
            WindowSystem.Draw();
        }

        private List<CharacterContainer> LoadData()
        {
            var characterContainers = Configuration.CharacterContainers ?? new List<CharacterContainer>(); //null on new install
            if (ClientState.LocalPlayer != null)
            {
                var loggedCharacter = $"{ClientState.LocalPlayer.Name}@{ClientState.LocalPlayer.HomeWorld.GameData.Name}";

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
