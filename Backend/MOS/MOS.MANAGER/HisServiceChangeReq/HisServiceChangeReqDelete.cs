using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceChangeReq
{
    partial class HisServiceChangeReqDelete : BusinessBase
    {
        internal HisServiceChangeReqDelete()
            : base()
        {

        }

        internal HisServiceChangeReqDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SERVICE_CHANGE_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceChangeReqCheck checker = new HisServiceChangeReqCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_CHANGE_REQ raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisServiceChangeReqDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SERVICE_CHANGE_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceChangeReqCheck checker = new HisServiceChangeReqCheck(param);
                List<HIS_SERVICE_CHANGE_REQ> listRaw = new List<HIS_SERVICE_CHANGE_REQ>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisServiceChangeReqDAO.DeleteList(listData);
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
