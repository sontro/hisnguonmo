using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisProcessingMethod
{
    partial class HisProcessingMethodDelete : BusinessBase
    {
        internal HisProcessingMethodDelete()
            : base()
        {

        }

        internal HisProcessingMethodDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_PROCESSING_METHOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisProcessingMethodCheck checker = new HisProcessingMethodCheck(param);
                valid = valid && IsNotNull(data);
                HIS_PROCESSING_METHOD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisProcessingMethodDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_PROCESSING_METHOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisProcessingMethodCheck checker = new HisProcessingMethodCheck(param);
                List<HIS_PROCESSING_METHOD> listRaw = new List<HIS_PROCESSING_METHOD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisProcessingMethodDAO.DeleteList(listData);
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
