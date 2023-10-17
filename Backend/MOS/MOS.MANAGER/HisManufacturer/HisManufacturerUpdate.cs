using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisManufacturer
{
    class HisManufacturerUpdate : BusinessBase
    {
        internal HisManufacturerUpdate()
            : base()
        {

        }

        internal HisManufacturerUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MANUFACTURER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisManufacturerCheck checker = new HisManufacturerCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.ExistsCode(data.MANUFACTURER_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisManufacturerDAO.Update(data);
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

        internal bool UpdateList(List<HIS_MANUFACTURER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisManufacturerCheck checker = new HisManufacturerCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.ExistsCode(data.MANUFACTURER_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisManufacturerDAO.UpdateList(listData);
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
