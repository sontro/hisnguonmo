using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestMetyReq
{
    partial class HisExpMestMetyReqDelete : BusinessBase
    {
        internal HisExpMestMetyReqDelete()
            : base()
        {

        }

        internal HisExpMestMetyReqDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EXP_MEST_METY_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestMetyReqCheck checker = new HisExpMestMetyReqCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_MEST_METY_REQ raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisExpMestMetyReqDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EXP_MEST_METY_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestMetyReqCheck checker = new HisExpMestMetyReqCheck(param);
                List<HIS_EXP_MEST_METY_REQ> listRaw = new List<HIS_EXP_MEST_METY_REQ>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisExpMestMetyReqDAO.DeleteList(listData);
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
