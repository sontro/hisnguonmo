using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisReportTypeCat
{
    partial class HisReportTypeCatDelete : BusinessBase
    {
        internal HisReportTypeCatDelete()
            : base()
        {

        }

        internal HisReportTypeCatDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_REPORT_TYPE_CAT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisReportTypeCatCheck checker = new HisReportTypeCatCheck(param);
                valid = valid && IsNotNull(data);
                HIS_REPORT_TYPE_CAT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisReportTypeCatDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_REPORT_TYPE_CAT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisReportTypeCatCheck checker = new HisReportTypeCatCheck(param);
                List<HIS_REPORT_TYPE_CAT> listRaw = new List<HIS_REPORT_TYPE_CAT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisReportTypeCatDAO.DeleteList(listData);
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
