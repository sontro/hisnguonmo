using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSubclinicalRsAdd
{
    partial class HisSubclinicalRsAddDelete : BusinessBase
    {
        internal HisSubclinicalRsAddDelete()
            : base()
        {

        }

        internal HisSubclinicalRsAddDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SUBCLINICAL_RS_ADD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSubclinicalRsAddCheck checker = new HisSubclinicalRsAddCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SUBCLINICAL_RS_ADD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisSubclinicalRsAddDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SUBCLINICAL_RS_ADD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSubclinicalRsAddCheck checker = new HisSubclinicalRsAddCheck(param);
                List<HIS_SUBCLINICAL_RS_ADD> listRaw = new List<HIS_SUBCLINICAL_RS_ADD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisSubclinicalRsAddDAO.DeleteList(listData);
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
