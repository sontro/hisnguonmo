using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSeseTransReq
{
    partial class HisSeseTransReqDelete : BusinessBase
    {
        internal HisSeseTransReqDelete()
            : base()
        {

        }

        internal HisSeseTransReqDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SESE_TRANS_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSeseTransReqCheck checker = new HisSeseTransReqCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SESE_TRANS_REQ raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisSeseTransReqDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SESE_TRANS_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSeseTransReqCheck checker = new HisSeseTransReqCheck(param);
                List<HIS_SESE_TRANS_REQ> listRaw = new List<HIS_SESE_TRANS_REQ>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisSeseTransReqDAO.DeleteList(listData);
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
