using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBcsMetyReqDt
{
    partial class HisBcsMetyReqDtDelete : BusinessBase
    {
        internal HisBcsMetyReqDtDelete()
            : base()
        {

        }

        internal HisBcsMetyReqDtDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_BCS_METY_REQ_DT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBcsMetyReqDtCheck checker = new HisBcsMetyReqDtCheck(param);
                valid = valid && IsNotNull(data);
                HIS_BCS_METY_REQ_DT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisBcsMetyReqDtDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_BCS_METY_REQ_DT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBcsMetyReqDtCheck checker = new HisBcsMetyReqDtCheck(param);
                List<HIS_BCS_METY_REQ_DT> listRaw = new List<HIS_BCS_METY_REQ_DT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisBcsMetyReqDtDAO.DeleteList(listData);
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
