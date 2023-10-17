using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.LocalStorage.SdaConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterExamKiosk
{
    class CheckHeinCardCFG
    {
        private const string CONFIG_KEY = "HIS.Desktop.Plugins.Register.IsCheckHeinCard";
        private const string IS_CHECK = "1";

        private static bool? isCheckHeinCard;
        public static bool IsCheckHeinCard
        {
            get
            {
                if (!isCheckHeinCard.HasValue)
                {
                    isCheckHeinCard = GetIsCheck(HisConfigs.Get<string>(CONFIG_KEY));
                }
                return isCheckHeinCard.Value;
            }
        }

        private static bool GetIsCheck(string value)
        {
            return (value == IS_CHECK);
        }
    }
}
