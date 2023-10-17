using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExeServiceModule
{
    partial class HisExeServiceModuleDelete : BusinessBase
    {
        internal HisExeServiceModuleDelete()
            : base()
        {

        }

        internal HisExeServiceModuleDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EXE_SERVICE_MODULE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExeServiceModuleCheck checker = new HisExeServiceModuleCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EXE_SERVICE_MODULE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisExeServiceModuleDAO.Delete(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool DeleteList(List<HIS_EXE_SERVICE_MODULE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExeServiceModuleCheck checker = new HisExeServiceModuleCheck(param);
                List<HIS_EXE_SERVICE_MODULE> listRaw = new List<HIS_EXE_SERVICE_MODULE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisExeServiceModuleDAO.DeleteList(listData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
