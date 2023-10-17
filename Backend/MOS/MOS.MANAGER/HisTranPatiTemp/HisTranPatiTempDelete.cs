using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTranPatiTemp
{
    partial class HisTranPatiTempDelete : BusinessBase
    {
        internal HisTranPatiTempDelete()
            : base()
        {

        }

        internal HisTranPatiTempDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_TRAN_PATI_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTranPatiTempCheck checker = new HisTranPatiTempCheck(param);
                valid = valid && IsNotNull(data);
                HIS_TRAN_PATI_TEMP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisTranPatiTempDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_TRAN_PATI_TEMP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTranPatiTempCheck checker = new HisTranPatiTempCheck(param);
                List<HIS_TRAN_PATI_TEMP> listRaw = new List<HIS_TRAN_PATI_TEMP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisTranPatiTempDAO.DeleteList(listData);
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
