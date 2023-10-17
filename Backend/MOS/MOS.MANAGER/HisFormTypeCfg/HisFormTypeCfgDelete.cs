using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisFormTypeCfg
{
    partial class HisFormTypeCfgDelete : BusinessBase
    {
        internal HisFormTypeCfgDelete()
            : base()
        {

        }

        internal HisFormTypeCfgDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_FORM_TYPE_CFG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisFormTypeCfgCheck checker = new HisFormTypeCfgCheck(param);
                valid = valid && IsNotNull(data);
                HIS_FORM_TYPE_CFG raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisFormTypeCfgDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_FORM_TYPE_CFG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisFormTypeCfgCheck checker = new HisFormTypeCfgCheck(param);
                List<HIS_FORM_TYPE_CFG> listRaw = new List<HIS_FORM_TYPE_CFG>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisFormTypeCfgDAO.DeleteList(listData);
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
