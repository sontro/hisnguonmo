using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceUnit
{
    class HisServiceUnitUpdate : BusinessBase
    {
        internal HisServiceUnitUpdate()
            : base()
        {

        }

        internal HisServiceUnitUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERVICE_UNIT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceUnitCheck checker = new HisServiceUnitCheck(param);
                HIS_SERVICE_UNIT before = null;
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.VerifyId(data.ID, ref before);
                valid = valid && checker.ExistsCode(data.SERVICE_UNIT_CODE, data.ID);
                valid = valid && checker.IsAllowUpdate(data, before);
                if (valid)
                {
                    result = DAOWorker.HisServiceUnitDAO.Update(data);
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

        internal bool UpdateList(List<HIS_SERVICE_UNIT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceUnitCheck checker = new HisServiceUnitCheck(param);
                foreach (var data in listData)
                {
                    HIS_SERVICE_UNIT before = null;
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.VerifyId(data.ID, ref before);
                    valid = valid && checker.IsAllowUpdate(data, before);
                    valid = valid && checker.ExistsCode(data.SERVICE_UNIT_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisServiceUnitDAO.UpdateList(listData);
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
