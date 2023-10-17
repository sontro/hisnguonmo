using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCaroDepartment
{
    partial class HisCaroDepartmentDelete : BusinessBase
    {
        internal HisCaroDepartmentDelete()
            : base()
        {

        }

        internal HisCaroDepartmentDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_CARO_DEPARTMENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCaroDepartmentCheck checker = new HisCaroDepartmentCheck(param);
                valid = valid && IsNotNull(data);
                HIS_CARO_DEPARTMENT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisCaroDepartmentDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_CARO_DEPARTMENT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCaroDepartmentCheck checker = new HisCaroDepartmentCheck(param);
                List<HIS_CARO_DEPARTMENT> listRaw = new List<HIS_CARO_DEPARTMENT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisCaroDepartmentDAO.DeleteList(listData);
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
