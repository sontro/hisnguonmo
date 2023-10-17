using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBidMaterialType
{
    partial class HisBidMaterialTypeTruncate : BusinessBase
    {
        internal HisBidMaterialTypeTruncate()
            : base()
        {

        }

        internal HisBidMaterialTypeTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_BID_MATERIAL_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBidMaterialTypeCheck checker = new HisBidMaterialTypeCheck(param);
                valid = valid && IsNotNull(data);
                HIS_BID_MATERIAL_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisBidMaterialTypeDAO.Truncate(data);
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

        internal bool TruncateList(List<HIS_BID_MATERIAL_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBidMaterialTypeCheck checker = new HisBidMaterialTypeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisBidMaterialTypeDAO.TruncateList(listData);
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
                List<HIS_BID_MATERIAL_TYPE> hisBidMaterialTypes = new HisBidMaterialTypeGet().GetByBidId(bidId);
                if (IsNotNullOrEmpty(hisBidMaterialTypes))
                {
                    result = this.TruncateList(hisBidMaterialTypes);
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
