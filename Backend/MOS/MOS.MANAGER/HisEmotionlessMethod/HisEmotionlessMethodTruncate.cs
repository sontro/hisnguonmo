using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEmotionlessMethod
{
    partial class HisEmotionlessMethodTruncate : BusinessBase
    {
        internal HisEmotionlessMethodTruncate()
            : base()
        {

        }

        internal HisEmotionlessMethodTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_EMOTIONLESS_METHOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmotionlessMethodCheck checker = new HisEmotionlessMethodCheck(param);
                valid = valid && IsNotNull(data);
                HIS_EMOTIONLESS_METHOD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisEmotionlessMethodDAO.Truncate(data);
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
