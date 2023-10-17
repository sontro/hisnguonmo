using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSuimIndexUnit
{
    class HisSuimIndexUnitUpdate : BusinessBase
    {
        internal HisSuimIndexUnitUpdate()
            : base()
        {

        }

        internal HisSuimIndexUnitUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SUIM_INDEX_UNIT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSuimIndexUnitCheck checker = new HisSuimIndexUnitCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data);
                valid = valid && checker.ExistsCode(data.SUIM_INDEX_UNIT_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisSuimIndexUnitDAO.Update(data);
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

        internal bool UpdateList(List<HIS_SUIM_INDEX_UNIT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSuimIndexUnitCheck checker = new HisSuimIndexUnitCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data);
                    valid = valid && checker.ExistsCode(data.SUIM_INDEX_UNIT_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisSuimIndexUnitDAO.UpdateList(listData);
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
