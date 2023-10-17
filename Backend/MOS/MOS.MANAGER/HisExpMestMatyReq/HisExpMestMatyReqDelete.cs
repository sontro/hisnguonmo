using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestMatyReq
{
    partial class HisExpMestMatyReqDelete : BusinessBase
    {
        internal HisExpMestMatyReqDelete()
            : base()
        {

        }

        internal HisExpMestMatyReqDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EXP_MEST_MATY_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestMatyReqCheck checker = new HisExpMestMatyReqCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_MEST_MATY_REQ raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisExpMestMatyReqDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EXP_MEST_MATY_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestMatyReqCheck checker = new HisExpMestMatyReqCheck(param);
                List<HIS_EXP_MEST_MATY_REQ> listRaw = new List<HIS_EXP_MEST_MATY_REQ>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisExpMestMatyReqDAO.DeleteList(listData);
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
