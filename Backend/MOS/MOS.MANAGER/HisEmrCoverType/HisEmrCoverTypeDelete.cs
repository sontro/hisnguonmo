using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEmrCoverType
{
    partial class HisEmrCoverTypeDelete : BusinessBase
    {
        internal HisEmrCoverTypeDelete()
            : base()
        {

        }

        internal HisEmrCoverTypeDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_EMR_COVER_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmrCoverTypeCheck checker = new HisEmrCoverTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EMR_COVER_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisEmrCoverTypeDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_EMR_COVER_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEmrCoverTypeCheck checker = new HisEmrCoverTypeCheck(param);
                List<HIS_EMR_COVER_TYPE> listRaw = new List<HIS_EMR_COVER_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisEmrCoverTypeDAO.DeleteList(listData);
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
