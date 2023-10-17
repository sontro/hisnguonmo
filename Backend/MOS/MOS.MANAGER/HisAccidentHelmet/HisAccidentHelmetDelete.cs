using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAccidentHelmet
{
    partial class HisAccidentHelmetDelete : BusinessBase
    {
        internal HisAccidentHelmetDelete()
            : base()
        {

        }

        internal HisAccidentHelmetDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ACCIDENT_HELMET data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccidentHelmetCheck checker = new HisAccidentHelmetCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ACCIDENT_HELMET raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisAccidentHelmetDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_ACCIDENT_HELMET> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAccidentHelmetCheck checker = new HisAccidentHelmetCheck(param);
                List<HIS_ACCIDENT_HELMET> listRaw = new List<HIS_ACCIDENT_HELMET>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisAccidentHelmetDAO.DeleteList(listData);
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
