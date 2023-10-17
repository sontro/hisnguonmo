using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceRetyCat;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisReportTypeCat
{
    partial class HisReportTypeCatTruncate : BusinessBase
    {
        internal HisReportTypeCatTruncate()
            : base()
        {

        }

        internal HisReportTypeCatTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_REPORT_TYPE_CAT data)
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
                    if (new HisServiceRetyCatTruncate(param).TruncateByReportTypeCatId(raw.ID))
                    {
                        result = DAOWorker.HisReportTypeCatDAO.Truncate(data);
                    }
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
