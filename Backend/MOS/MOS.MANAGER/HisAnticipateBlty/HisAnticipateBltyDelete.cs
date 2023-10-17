using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAnticipateBlty
{
    partial class HisAnticipateBltyDelete : BusinessBase
    {
        internal HisAnticipateBltyDelete()
            : base()
        {

        }

        internal HisAnticipateBltyDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_ANTICIPATE_BLTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAnticipateBltyCheck checker = new HisAnticipateBltyCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ANTICIPATE_BLTY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisAnticipateBltyDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_ANTICIPATE_BLTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAnticipateBltyCheck checker = new HisAnticipateBltyCheck(param);
                List<HIS_ANTICIPATE_BLTY> listRaw = new List<HIS_ANTICIPATE_BLTY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisAnticipateBltyDAO.DeleteList(listData);
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
