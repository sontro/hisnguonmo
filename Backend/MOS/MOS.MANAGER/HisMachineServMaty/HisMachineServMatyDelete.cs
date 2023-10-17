using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMachineServMaty
{
    partial class HisMachineServMatyDelete : BusinessBase
    {
        internal HisMachineServMatyDelete()
            : base()
        {

        }

        internal HisMachineServMatyDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_MACHINE_SERV_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMachineServMatyCheck checker = new HisMachineServMatyCheck(param);
                valid = valid && IsNotNull(data);
                HIS_MACHINE_SERV_MATY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisMachineServMatyDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_MACHINE_SERV_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMachineServMatyCheck checker = new HisMachineServMatyCheck(param);
                List<HIS_MACHINE_SERV_MATY> listRaw = new List<HIS_MACHINE_SERV_MATY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisMachineServMatyDAO.DeleteList(listData);
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
