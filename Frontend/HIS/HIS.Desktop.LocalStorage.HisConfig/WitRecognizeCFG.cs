using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.HisConfig
{
    public class WitRecognizeCFG
    {
        private const string CONFIG_KEY__WitAi_AccessToken = "Inventec.Common.WitAI.WitAiAccessToken";
        private const string CONFIG_KEY__WitAI_TimeReplay = "Inventec.Common.WitAI.TimeReplay";
        public static string AccessToken;
        public static int TimeReplay;

        public static void LoadConfig()
        {
            try
            {
                AccessToken = HisConfigs.Get<string>(CONFIG_KEY__WitAi_AccessToken);
                TimeReplay = int.Parse(HisConfigs.Get<string>(CONFIG_KEY__WitAI_TimeReplay) ?? "3000");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
