using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisWelfareType
{
    partial class HisWelfareTypeDelete : BusinessBase
    {
        internal HisWelfareTypeDelete()
            : base()
        {

        }

        internal HisWelfareTypeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_WELFARE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisWelfareTypeCheck checker = new HisWelfareTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_WELFARE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisWelfareTypeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_WELFARE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisWelfareTypeCheck checker = new HisWelfareTypeCheck(param);
                List<HIS_WELFARE_TYPE> listRaw = new List<HIS_WELFARE_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisWelfareTypeDAO.DeleteList(listData);
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
