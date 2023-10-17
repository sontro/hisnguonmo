using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisManufacturer
{
    class HisManufacturerTruncate : BusinessBase
    {
        internal HisManufacturerTruncate()
            : base()
        {

        }

        internal HisManufacturerTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_MANUFACTURER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisManufacturerCheck checker = new HisManufacturerCheck(param);
                valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.CheckConstraint(data.ID);
                if (valid)
                {
                    result = DAOWorker.HisManufacturerDAO.Truncate(data);
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
                HisManufacturerCheck checker = new HisManufacturerCheck(param);
                List<HIS_MANUFACTURER> listRaw = new List<HIS_MANUFACTURER>();
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

        internal bool TruncateList(List<HIS_MANUFACTURER> listRaw)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listRaw);
                HisManufacturerCheck checker = new HisManufacturerCheck(param);
                valid = valid && checker.IsUnLock(listRaw);

                if (valid)
                {
                    result = DAOWorker.HisManufacturerDAO.TruncateList(listRaw);
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
