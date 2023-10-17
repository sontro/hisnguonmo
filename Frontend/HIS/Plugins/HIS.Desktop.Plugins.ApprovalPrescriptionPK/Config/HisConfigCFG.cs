using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ApprovalPrescriptionPK.Config
{
    class HisConfigCFG
    {
        private const string CONFIG_KEY__EXP_MEST_TYPE_THPK_AUTO_CREATE_AGGR_EXAM_EXP_MEST = "HIS_EXP_MEST.EXP_MEST_TYPE.THPK.AUTO_CREATE_AGGR_EXAM_EXP_MEST";

        internal static string AutoCreateAggrExamExpMest;

        internal static void LoadConfig()
        {
            try
            {
                AutoCreateAggrExamExpMest = GetValue(CONFIG_KEY__EXP_MEST_TYPE_THPK_AUTO_CREATE_AGGR_EXAM_EXP_MEST);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);                
            }
        }

        private static string GetValue(string code)
        {
            string result = null;
            try
            {
                return HisConfigs.Get<string>(code);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
                result = null;
            }
            return result;
        }


    }
}
