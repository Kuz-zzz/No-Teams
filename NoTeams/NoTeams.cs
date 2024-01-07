using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using Newtonsoft.Json;



namespace NoTeams
{
    [ApiVersion(2, 1)]
    public class NoTeams : TerrariaPlugin
    {
        public override string Author => "Kuz_";

        public override string Description => "Prevents players from joining teams";

        public override string Name => "No Teams";

        public override Version Version => new Version(1, 1, 0, 0);

        private static Config config;
        internal static string filepath { get { return Path.Combine(TShock.SavePath, "noteams.json"); } }
        public static bool NoTeamsOn = true;

        public NoTeams(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            GetDataHandlers.PlayerTeam += OnTeamChange;
            ReadConfig(filepath, Config.DefaultConfig(), out config);
            Commands.ChatCommands.Add(new Command("tshock.noteams", CommandHandler, "noteams"));
            if (config.enabled)
            {
                NoTeamsOn = true;
            } else
            {
                NoTeamsOn = false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                GetDataHandlers.PlayerTeam -= OnTeamChange;
            }
            base.Dispose(disposing);
        }

        private static void ReadConfig<TConfig>(string path, TConfig defaultConfig, out TConfig config)
        {
            if (!File.Exists(path))
            {
                config = defaultConfig;
                File.WriteAllText(path, JsonConvert.SerializeObject(config, Formatting.Indented));
            }
            else
            {
                config = JsonConvert.DeserializeObject<TConfig>(File.ReadAllText(path));

            }
        }

        private void CommandHandler(CommandArgs args)
        {
            if (args.Parameters.Count != 0)
            {
                if (args.Parameters[0] == "on")
                {
                    NoTeamsOn = true;
                    args.Player.SendSuccessMessage("Players can no longer switch teams!");
                }
                else if (args.Parameters[0] == "off")
                {
                    NoTeamsOn = false;
                    args.Player.SendSuccessMessage("Players now can switch teams!");
                } 
                else if (args.Parameters[0] == "reload")
                {
                    ReadConfig(filepath, Config.DefaultConfig(), out config);
                    args.Player.SendSuccessMessage("Config was successfully reloaded");
                }
                else {
                    args.Player.SendErrorMessage("Invalid syntaxis! Usage: /noteams <on/off>");
                }
            }
            else
            {
                args.Player.SendErrorMessage("Invalid syntaxis! Usage: /noteams <on/off>");
            }
        }

        void OnTeamChange(object sender, GetDataHandlers.PlayerTeamEventArgs args)
        {
            if (NoTeamsOn)
            {
                TSPlayer player = args.Player;

                if (config.kickOnSwitchAttempt)
                {
                    player.Kick(config.message);
                    return;
                }

                player.SetTeam(0); // sets player's team back to white
                player.SendErrorMessage(config.message); // sends a message that can only be seen by the player

                args.Handled = true; // makes it so "PlayerName joined the TeamName party!" doesn't appear
            }            
        }
    }
}
