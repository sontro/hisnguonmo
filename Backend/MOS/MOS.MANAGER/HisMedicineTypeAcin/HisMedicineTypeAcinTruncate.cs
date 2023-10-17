using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineTypeAcin
{
    class HisMedicineTypeAcinTruncate : BusinessBase
    {
        internal HisMedicineTypeAcinTruncate()
            : base()
        {

        }

        internal HisMedicineTypeAcinTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_MEDICINE_TYPE_ACIN data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineTypeAcinCheck checker = new HisMedicineTypeAcinCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisMedicineTypeAcinDAO.Truncate(data);
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

        internal bool TruncateList(List<long> ids)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(ids);
                HisMedicineTypeAcinCheck checker = new HisMedicineTypeAcinCheck(param);
                List<HIS_MEDICINE_TYPE_ACIN> listRaw = new List<HIS_MEDICINE_TYPE_ACIN>();
                valid = valid && checker.VerifyIds(ids, listRaw);
                if (valid)
                {
                    result = this.TruncateList(listRaw);
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

        internal bool TruncateList(List<HIS_MEDICINE_TYPE_ACIN> listRaw)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listRaw);
                HisMedicineTypeAcinCheck checker = new HisMedicineTypeAcinCheck(param);
                valid = valid && checker.IsUnLock(listRaw);

                if (valid)
                {
                    result = DAOWorker.HisMedicineTypeAcinDAO.TruncateList(listRaw);
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
