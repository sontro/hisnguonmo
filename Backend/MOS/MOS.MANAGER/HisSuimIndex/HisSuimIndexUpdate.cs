using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSuimIndex
{
    class HisSuimIndexUpdate : BusinessBase
    {
        internal HisSuimIndexUpdate()
            : base()
        {

        }

        internal HisSuimIndexUpdate(Inventec.Core.CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SUIM_INDEX data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSuimIndexCheck checker = new HisSuimIndexCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.ExistsCode(data.SUIM_INDEX_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisSuimIndexDAO.Update(data);
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

        internal bool UpdateList(List<HIS_SUIM_INDEX> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSuimIndexCheck checker = new HisSuimIndexCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.ExistsCode(data.SUIM_INDEX_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisSuimIndexDAO.UpdateList(listData);
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
