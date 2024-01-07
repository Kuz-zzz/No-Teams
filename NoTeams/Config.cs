namespace NoTeams
{
    public class Config
    {
        public bool enabled;
        public string message = "";
        public bool kickOnSwitchAttempt;

        public static Config DefaultConfig()
        {
            Config vConf = new Config
            {
                enabled = true,
                message = "You cannot join teams!",
                kickOnSwitchAttempt = false,
            };

            return vConf;
        }
    }
}
