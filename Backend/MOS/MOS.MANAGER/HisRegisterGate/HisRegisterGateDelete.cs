using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRegisterGate
{
    partial class HisRegisterGateDelete : BusinessBase
    {
        internal HisRegisterGateDelete()
            : base()
        {

        }

        internal HisRegisterGateDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_REGISTER_GATE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRegisterGateCheck checker = new HisRegisterGateCheck(param);
                valid = valid && IsNotNull(data);
                HIS_REGISTER_GATE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisRegisterGateDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_REGISTER_GATE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRegisterGateCheck checker = new HisRegisterGateCheck(param);
                List<HIS_REGISTER_GATE> listRaw = new List<HIS_REGISTER_GATE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisRegisterGateDAO.DeleteList(listData);
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
