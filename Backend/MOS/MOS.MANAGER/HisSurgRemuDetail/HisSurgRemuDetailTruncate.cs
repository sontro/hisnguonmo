using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSurgRemuDetail
{
    partial class HisSurgRemuDetailTruncate : BusinessBase
    {
        internal HisSurgRemuDetailTruncate()
            : base()
        {

        }

        internal HisSurgRemuDetailTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSurgRemuDetailCheck checker = new HisSurgRemuDetailCheck(param);
                HIS_SURG_REMU_DETAIL raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    result = DAOWorker.HisSurgRemuDetailDAO.Truncate(raw);
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

        internal bool TruncateBySurgRemunerationId(long surgRemunerationId)
        {
            bool result = false;
            try
            {
                List<HIS_SURG_REMU_DETAIL> details = new HisSurgRemuDetailGet().GetBySurgRemunerationId(surgRemunerationId);
                if (IsNotNullOrEmpty(details))
                {
                    return new HisSurgRemuDetailTruncate().TruncateList(details);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool TruncateList(List<HIS_SURG_REMU_DETAIL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSurgRemuDetailCheck checker = new HisSurgRemuDetailCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
					valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisSurgRemuDetailDAO.TruncateList(listData);
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
