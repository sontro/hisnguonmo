using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBcsMatyReqReq
{
    partial class HisBcsMatyReqReqDelete : BusinessBase
    {
        internal HisBcsMatyReqReqDelete()
            : base()
        {

        }

        internal HisBcsMatyReqReqDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_BCS_MATY_REQ_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBcsMatyReqReqCheck checker = new HisBcsMatyReqReqCheck(param);
                valid = valid && IsNotNull(data);
                HIS_BCS_MATY_REQ_REQ raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisBcsMatyReqReqDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_BCS_MATY_REQ_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBcsMatyReqReqCheck checker = new HisBcsMatyReqReqCheck(param);
                List<HIS_BCS_MATY_REQ_REQ> listRaw = new List<HIS_BCS_MATY_REQ_REQ>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisBcsMatyReqReqDAO.DeleteList(listData);
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
