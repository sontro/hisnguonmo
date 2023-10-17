using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBidBloodType;
using MOS.MANAGER.HisBidMaterialType;
using MOS.MANAGER.HisBidMedicineType;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBid
{
    partial class HisBidTruncate : BusinessBase
    {
        internal HisBidTruncate()
            : base()
        {

        }

        internal HisBidTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_BID data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBidCheck checker = new HisBidCheck(param);
                valid = valid && IsNotNull(data);
                HIS_BID raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    if (!new HisBidMedicineTypeTruncate(param).TruncateByBidId(raw.ID))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                    if (!new HisBidMaterialTypeTruncate(param).TruncateByBidId(raw.ID))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                    if (!new HisBidBloodTypeTruncate(param).TruncateByBidId(raw.ID))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                    result = DAOWorker.HisBidDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_BID> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBidCheck checker = new HisBidCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisBidDAO.TruncateList(listData);
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
