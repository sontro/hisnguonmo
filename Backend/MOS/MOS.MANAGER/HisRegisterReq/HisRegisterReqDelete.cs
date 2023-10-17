using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRegisterReq
{
    partial class HisRegisterReqDelete : BusinessBase
    {
        internal HisRegisterReqDelete()
            : base()
        {

        }

        internal HisRegisterReqDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_REGISTER_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRegisterReqCheck checker = new HisRegisterReqCheck(param);
                valid = valid && IsNotNull(data);
                HIS_REGISTER_REQ raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisRegisterReqDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_REGISTER_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRegisterReqCheck checker = new HisRegisterReqCheck(param);
                List<HIS_REGISTER_REQ> listRaw = new List<HIS_REGISTER_REQ>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisRegisterReqDAO.DeleteList(listData);
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
