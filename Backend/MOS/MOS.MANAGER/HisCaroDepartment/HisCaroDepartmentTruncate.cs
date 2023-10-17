using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisRestRetrType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCaroDepartment
{
    partial class HisCaroDepartmentTruncate : BusinessBase
    {
        internal HisCaroDepartmentTruncate()
            : base()
        {

        }

        internal HisCaroDepartmentTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCaroDepartmentCheck checker = new HisCaroDepartmentCheck(param);
                HIS_CARO_DEPARTMENT raw = null;
                valid = valid && checker.VerifyId(id, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckConstraint(id);
                if (valid)
                {
                    result = DAOWorker.HisCaroDepartmentDAO.Truncate(raw);
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
                valid = valid && IsNotNullOrEmpty(ids);
                HisCaroDepartmentCheck checker = new HisCaroDepartmentCheck(param);
                List<HIS_CARO_DEPARTMENT> listRaw = new List<HIS_CARO_DEPARTMENT>();
                valid = valid && checker.VerifyIds(ids, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisCaroDepartmentDAO.TruncateList(listRaw);
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

        internal bool TruncateList(List<HIS_CARO_DEPARTMENT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCaroDepartmentCheck checker = new HisCaroDepartmentCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && IsNotNull(data) && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.CheckConstraint(data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisCaroDepartmentDAO.TruncateList(listData);
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

        internal bool TruncateByCashierRoomId(long id)
        {
            bool result = true;
            try
            {
                List<HIS_CARO_DEPARTMENT> hisCaroDepartments = new HisCaroDepartmentGet().GetByCashierRoomId(id);
                if (IsNotNullOrEmpty(hisCaroDepartments))
                {
                    result = this.TruncateList(hisCaroDepartments);
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
