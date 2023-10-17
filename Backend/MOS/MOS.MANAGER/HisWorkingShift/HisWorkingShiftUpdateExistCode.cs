using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisWorkingShift
{
    partial class HisWorkingShiftUpdate : BusinessBase
    {
		private List<HIS_WORKING_SHIFT> beforeUpdateHisWorkingShifts = new List<HIS_WORKING_SHIFT>();
		
        internal HisWorkingShiftUpdate()
            : base()
        {

        }

        internal HisWorkingShiftUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_WORKING_SHIFT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisWorkingShiftCheck checker = new HisWorkingShiftCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_WORKING_SHIFT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.WORKING_SHIFT_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisWorkingShiftDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisWorkingShift_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisWorkingShift that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisWorkingShifts.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_WORKING_SHIFT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisWorkingShiftCheck checker = new HisWorkingShiftCheck(param);
                List<HIS_WORKING_SHIFT> listRaw = new List<HIS_WORKING_SHIFT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.WORKING_SHIFT_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisWorkingShiftDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisWorkingShift_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisWorkingShift that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisWorkingShifts.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisWorkingShifts))
            {
                if (!DAOWorker.HisWorkingShiftDAO.UpdateList(this.beforeUpdateHisWorkingShifts))
                {
                    LogSystem.Warn("Rollback du lieu HisWorkingShift that bai, can kiem tra lai." + LogUtil.TraceData("HisWorkingShifts", this.beforeUpdateHisWorkingShifts));
                }
				this.beforeUpdateHisWorkingShifts = null;
            }
        }
    }
}
