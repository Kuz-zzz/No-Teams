using Terraria;
using TerrariaApi.Server;
using TShockAPI;


namespace NoTeams
{
    [ApiVersion(2, 1)]
    public class NoTeams : TerrariaPlugin
    {
        public override string Author => "Kuz_";

        public override string Description => "Prevents players from joining teams";

        public override string Name => "No Teams";

        public override Version Version => new Version(1, 0, 0, 0);

        public NoTeams(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            GetDataHandlers.PlayerTeam += OnTeamChange;

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                GetDataHandlers.PlayerTeam -= OnTeamChange;
            }
            base.Dispose(disposing);
        }

        void OnTeamChange(object sender, GetDataHandlers.PlayerTeamEventArgs args)
        {
            TSPlayer tSPlayer = args.Player;

            tSPlayer.SetTeam(0); // sets player's team back to white
            tSPlayer.SendErrorMessage("You cannot join teams!"); // sends a message that can only be seen by the player

            args.Handled = true; // makes it so "PlayerName joined the TeamName party!" doesn't appear
        }
    }
}
