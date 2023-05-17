using System.Collections.Generic;
using System.Linq;
using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using Altoholic.Windows;
using Dalamud.Game.ClientState;
using Dalamud.Interface.Windowing;

namespace Altoholic
{
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "Altoholic";
        private const string CommandName = "/alt";

        private DalamudPluginInterface PluginInterface { get; init; }
        private ClientState ClientState { get; init; }
        private CommandManager CommandManager { get; init; }
        public Configuration Configuration { get; init; }
        public WindowSystem WindowSystem = new("Altoholic");

        private MainWindow MainWindow { get; init; }

        public Plugin(
            [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
            [RequiredVersion("1.0")] CommandManager commandManager,
            [RequiredVersion("1.0")] ClientState clientState)
        {
            this.PluginInterface = pluginInterface;
            this.CommandManager = commandManager;
            this.ClientState = clientState;

            this.Configuration = this.PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            this.Configuration.Initialize(this.PluginInterface);

            MainWindow = new MainWindow();

            WindowSystem.AddWindow(MainWindow);

            this.CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
            {
                HelpMessage = "Opens the Altoholic View"
            });

            this.PluginInterface.UiBuilder.Draw += DrawUI;
        }

        public void Dispose()
        {
            this.WindowSystem.RemoveAllWindows();
            
            MainWindow.Dispose();
            
            this.CommandManager.RemoveHandler(CommandName);
        }

        private void OnCommand(string command, string args)
        {
            MainWindow.CharacterData = LoadData();
            MainWindow.IsOpen = true;
        }
 
        private void DrawUI()
        {
            this.WindowSystem.Draw();
        }

        private List<CharacterData> LoadData()
        {
            Configuration.Load();
            
            var characterData = Configuration.CharacterData;

            if (ClientState.LocalPlayer != null)
            {
                var loggedCharacter = $"{ClientState.LocalPlayer.Name.ToString()} @{ClientState.LocalPlayer.HomeWorld.GameData.Name}";

                var existingCharacter = characterData.FirstOrDefault(x => x.CharacterName == loggedCharacter);
                if (existingCharacter != null)
                {
                    existingCharacter.Refresh();
                }
                else
                {
                    characterData.Add(new CharacterData(loggedCharacter));
                }
            }

            Configuration.CharacterData = characterData;
            Configuration.Save();
            
            return characterData;
        }
    }
}
