using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisQcType
{
    partial class HisQcTypeDelete : BusinessBase
    {
        internal HisQcTypeDelete()
            : base()
        {

        }

        internal HisQcTypeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_QC_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisQcTypeCheck checker = new HisQcTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_QC_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisQcTypeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_QC_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisQcTypeCheck checker = new HisQcTypeCheck(param);
                List<HIS_QC_TYPE> listRaw = new List<HIS_QC_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisQcTypeDAO.DeleteList(listData);
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
