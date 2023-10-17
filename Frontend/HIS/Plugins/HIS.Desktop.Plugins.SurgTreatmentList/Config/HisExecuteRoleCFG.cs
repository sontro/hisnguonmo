using Inventec.Core;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SurgTreatmentList.Config
{
    class HisExecuteRoleCFG
    {
        private const string ListExecuteRole = "HIS.Desktop.Plugins.SurgTreatmentList.ListRole";

        internal static Dictionary<int, HIS_EXECUTE_ROLE> ProcessDicExecuteRole()
        {
            Dictionary<int, HIS_EXECUTE_ROLE> result = new Dictionary<int, HIS_EXECUTE_ROLE>();
            try
            {
                var listExecuteRoleCode = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(ListExecuteRole);
                if (!String.IsNullOrWhiteSpace(listExecuteRoleCode))
                {
                    var lstRole = GetRoleActive();
                    var arrRoleCode = listExecuteRoleCode.Split(';');
                    arrRoleCode = arrRoleCode.Distinct().ToArray();
                    for (int i = 0; i < arrRoleCode.Length; i++)
                    {
                        var role = lstRole.FirstOrDefault(o => o.EXECUTE_ROLE_CODE == arrRoleCode[i]);
                        if (role != null)
                        {
                            result.Add(i + 1, role);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private static List<HIS_EXECUTE_ROLE> GetRoleActive()
        {
            List<HIS_EXECUTE_ROLE> result = new List<HIS_EXECUTE_ROLE>();
            try
            {
                CommonParam param = new CommonParam();
                MOS.Filter.HisExecuteRoleFilter filter = new MOS.Filter.HisExecuteRoleFilter();
                filter.IS_ACTIVE = 1;
                result = new Inventec.Common.Adapter.BackendAdapter(param).Get<List<HIS_EXECUTE_ROLE>>("api/HisExecuteRole/Get", ApiConsumer.ApiConsumers.MosConsumer, filter, param);
            }
            catch (Exception ex)
            {
                result = new List<HIS_EXECUTE_ROLE>();
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}
