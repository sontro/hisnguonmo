using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServPtttTemp
{
    partial class HisSereServPtttTempDelete : BusinessBase
    {
        internal HisSereServPtttTempDelete()
            : base()
        {

        }

        internal HisSereServPtttTempDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SERE_SERV_PTTT_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServPtttTempCheck checker = new HisSereServPtttTempCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SERE_SERV_PTTT_TEMP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisSereServPtttTempDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SERE_SERV_PTTT_TEMP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServPtttTempCheck checker = new HisSereServPtttTempCheck(param);
                List<HIS_SERE_SERV_PTTT_TEMP> listRaw = new List<HIS_SERE_SERV_PTTT_TEMP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisSereServPtttTempDAO.DeleteList(listData);
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
