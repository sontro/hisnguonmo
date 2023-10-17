using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.VaccinationExam
{
    internal class CheckAdmin
    {
        public static bool IsAdmin(string loginName)
        {
            bool result = false;
            try
            {
                if (!string.IsNullOrEmpty(loginName))
                {
                    var _employee = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get<MOS.EFMODEL.DataModels.HIS_EMPLOYEE>().FirstOrDefault(p => p.LOGINNAME == loginName.Trim());
                    if (_employee != null && _employee.IS_ADMIN == (short)1)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
