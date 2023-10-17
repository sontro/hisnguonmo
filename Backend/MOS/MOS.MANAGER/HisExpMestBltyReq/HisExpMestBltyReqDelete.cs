using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestBltyReq
{
    partial class HisExpMestBltyReqDelete : BusinessBase
    {
        internal HisExpMestBltyReqDelete()
            : base()
        {

        }

        internal HisExpMestBltyReqDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EXP_MEST_BLTY_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestBltyReqCheck checker = new HisExpMestBltyReqCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_MEST_BLTY_REQ raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisExpMestBltyReqDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EXP_MEST_BLTY_REQ> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestBltyReqCheck checker = new HisExpMestBltyReqCheck(param);
                List<HIS_EXP_MEST_BLTY_REQ> listRaw = new List<HIS_EXP_MEST_BLTY_REQ>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisExpMestBltyReqDAO.DeleteList(listData);
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
