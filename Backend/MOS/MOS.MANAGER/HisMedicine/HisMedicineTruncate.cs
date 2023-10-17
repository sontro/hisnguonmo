using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMedicinePaty;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicine
{
    class HisMedicineTruncate : BusinessBase
    {
        internal HisMedicineTruncate()
            : base()
        {

        }

        internal HisMedicineTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_MEDICINE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineCheck checker = new HisMedicineCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    new HisMedicinePatyTruncate(param).TruncateByMedicineId(data.ID);
                    result = DAOWorker.HisMedicineDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_MEDICINE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineCheck checker = new HisMedicineCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data);
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    new HisMedicinePatyTruncate(param).TruncateByMedicineId(listData.Select(o => o.ID).ToList());
                    result = DAOWorker.HisMedicineDAO.TruncateList(listData);
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
