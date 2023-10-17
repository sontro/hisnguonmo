using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCaroDepartment
{
    partial class HisCaroDepartmentUpdate : BusinessBase
    {
		private List<HIS_CARO_DEPARTMENT> beforeUpdateHisCaroDepartments = new List<HIS_CARO_DEPARTMENT>();
		
        internal HisCaroDepartmentUpdate()
            : base()
        {

        }

        internal HisCaroDepartmentUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_CARO_DEPARTMENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCaroDepartmentCheck checker = new HisCaroDepartmentCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_CARO_DEPARTMENT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.CARO_DEPARTMENT_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisCaroDepartments.Add(raw);
					if (!DAOWorker.HisCaroDepartmentDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCaroDepartment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCaroDepartment that bai." + LogUtil.TraceData("data", data));
                    }
                    
                    result = true;
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

        internal bool UpdateList(List<HIS_CARO_DEPARTMENT> listData)
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
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.CARO_DEPARTMENT_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisCaroDepartments.AddRange(listRaw);
					if (!DAOWorker.HisCaroDepartmentDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCaroDepartment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCaroDepartment that bai." + LogUtil.TraceData("listData", listData));
                    }
                    result = true;
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
		
		internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisCaroDepartments))
            {
                if (!new HisCaroDepartmentUpdate(param).UpdateList(this.beforeUpdateHisCaroDepartments))
                {
                    LogSystem.Warn("Rollback du lieu HisCaroDepartment that bai, can kiem tra lai." + LogUtil.TraceData("HisCaroDepartments", this.beforeUpdateHisCaroDepartments));
                }
            }
        }
    }
}
