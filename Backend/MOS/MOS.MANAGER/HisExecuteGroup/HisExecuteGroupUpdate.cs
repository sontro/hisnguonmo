using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExecuteGroup
{
    class HisExecuteGroupUpdate : BusinessBase
    {
        internal HisExecuteGroupUpdate()
            : base()
        {

        }

        internal HisExecuteGroupUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EXECUTE_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExecuteGroupCheck checker = new HisExecuteGroupCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && IsGreaterThanZero(data.ID);
                valid = valid && checker.IsUnLock(data.ID);
                valid = valid && checker.ExistsCode(data.EXECUTE_GROUP_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisExecuteGroupDAO.Update(data);
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

        internal bool UpdateList(List<HIS_EXECUTE_GROUP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExecuteGroupCheck checker = new HisExecuteGroupCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.ExistsCode(data.EXECUTE_GROUP_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisExecuteGroupDAO.UpdateList(listData);
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
