using Inventec.Common.Logging;
using Inventec.Core;
using MOS.Filter;
using SDA.EFMODEL.DataModels;
using System;
using System.Linq;
using MOS.LibraryHein.Bhyt.HeinLevel;
using HIS.Desktop.LocalStorage.LocalData;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.LocalStorage.HisConfig
{
    public class HisHeinLevelCFG
    {
        private static string heinLevelCodeCurrent;
        public static string HEIN_LEVEL_CODE__CURRENT
        {
            get
            {
                //if (String.IsNullOrEmpty(heinLevelCodeCurrent))
                //{
                try
                {
                    heinLevelCodeCurrent = BranchCFG.Branch.HEIN_LEVEL_CODE;
                }
                catch (Exception ex)
                {
                    LogSystem.Error(ex);
                }
                //}
                return heinLevelCodeCurrent;
            }
            set
            {
                heinLevelCodeCurrent = value;
            }
        }
    }
}
