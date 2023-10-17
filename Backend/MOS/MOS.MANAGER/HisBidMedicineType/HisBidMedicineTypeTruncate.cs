using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBidMedicineType
{
    partial class HisBidMedicineTypeTruncate : BusinessBase
    {
        internal HisBidMedicineTypeTruncate()
            : base()
        {

        }

        internal HisBidMedicineTypeTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_BID_MEDICINE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBidMedicineTypeCheck checker = new HisBidMedicineTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_BID_MEDICINE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisBidMedicineTypeDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_BID_MEDICINE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBidMedicineTypeCheck checker = new HisBidMedicineTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisBidMedicineTypeDAO.TruncateList(listData);
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

        internal bool TruncateByBidId(long bidId)
        {
            bool result = true;
            try
            {
                List<HIS_BID_MEDICINE_TYPE> hisBidMedicineTypes = new HisBidMedicineTypeGet().GetByBidId(bidId);
                if (IsNotNullOrEmpty(hisBidMedicineTypes))
                {
                    result = this.TruncateList(hisBidMedicineTypes);
                }
                result = true;
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
