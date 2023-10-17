using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisCareDetail;
using MOS.MANAGER.HisDhst;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCare
{
    partial class HisCareTruncate : BusinessBase
    {
        internal HisCareTruncate()
            : base()
        {

        }

        internal HisCareTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_CARE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCareCheck checker = new HisCareCheck(param);
                valid = valid && IsNotNull(data);
                HIS_CARE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!new HisCareDetailTruncate(param).TruncateByCareId(data.ID))
                    {
                        throw new Exception("Xoa du lieu care-detail that bai");
                    }
                    if (!new HisDhstTruncate(param).TruncateByCareId(data.ID))
                    {
                        throw new Exception("Xoa du lieu dhst that bai");
                    }
                    result = DAOWorker.HisCareDAO.Truncate(data);
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
