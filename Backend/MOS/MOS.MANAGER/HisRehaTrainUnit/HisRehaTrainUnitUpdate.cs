using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRehaTrainUnit
{
    class HisRehaTrainUnitUpdate : BusinessBase
    {
        internal HisRehaTrainUnitUpdate()
            : base()
        {

        }

        internal HisRehaTrainUnitUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_REHA_TRAIN_UNIT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRehaTrainUnitCheck checker = new HisRehaTrainUnitCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.ExistsCode(data.REHA_TRAIN_UNIT_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisRehaTrainUnitDAO.Update(data);
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

        internal bool UpdateList(List<HIS_REHA_TRAIN_UNIT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRehaTrainUnitCheck checker = new HisRehaTrainUnitCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.ExistsCode(data.REHA_TRAIN_UNIT_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisRehaTrainUnitDAO.UpdateList(listData);
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
