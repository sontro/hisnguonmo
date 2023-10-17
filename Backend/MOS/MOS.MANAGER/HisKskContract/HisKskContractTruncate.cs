using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskContract
{
    partial class HisKskContractTruncate : BusinessBase
    {
        internal HisKskContractTruncate()
            : base()
        {

        }

        internal HisKskContractTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_KSK_CONTRACT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskContractCheck checker = new HisKskContractCheck(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_CONTRACT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisKskContractDAO.Truncate(data);
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
