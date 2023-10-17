using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDhst
{
    class HisDhstTruncate : BusinessBase
    {
        internal HisDhstTruncate()
            : base()
        {

        }

        internal HisDhstTruncate(Inventec.Core.CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_DHST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDhstCheck checker = new HisDhstCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisDhstDAO.Truncate(data);
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

        internal bool TruncateByCareId(long id)
        {
            bool result = true;
            try
            {
                List<HIS_DHST> hisDhsts = new HisDhstGet().GetByCareId(id);
                if (IsNotNullOrEmpty(hisDhsts))
                {
                    result = DAOWorker.HisDhstDAO.TruncateList(hisDhsts);
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
