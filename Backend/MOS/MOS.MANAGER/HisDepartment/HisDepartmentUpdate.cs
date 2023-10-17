using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDepartment
{
    class HisDepartmentUpdate : BusinessBase
    {
        internal HisDepartmentUpdate()
            : base()
        {

        }

        internal HisDepartmentUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_DEPARTMENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDepartmentCheck checker = new HisDepartmentCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DEPARTMENT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.DEPARTMENT_CODE, data.ID);
                valid = valid && checker.IsNotUpdatedBranch(data, raw);
                if (valid)
                {
                    result = DAOWorker.HisDepartmentDAO.Update(data);
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

        internal bool UpdateList(List<HIS_DEPARTMENT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDepartmentCheck checker = new HisDepartmentCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.ExistsCode(data.DEPARTMENT_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisDepartmentDAO.UpdateList(listData);
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
