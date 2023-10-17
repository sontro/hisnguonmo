using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisWorkingShift
{
    partial class HisWorkingShiftCreate : BusinessBase
    {
		private List<HIS_WORKING_SHIFT> recentHisWorkingShifts = new List<HIS_WORKING_SHIFT>();
		
        internal HisWorkingShiftCreate()
            : base()
        {

        }

        internal HisWorkingShiftCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_WORKING_SHIFT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisWorkingShiftCheck checker = new HisWorkingShiftCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisWorkingShiftDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisWorkingShift_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisWorkingShift that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisWorkingShifts.Add(data);
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
		
		internal bool CreateList(List<HIS_WORKING_SHIFT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisWorkingShiftCheck checker = new HisWorkingShiftCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisWorkingShiftDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisWorkingShift_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisWorkingShift that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisWorkingShifts.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisWorkingShifts))
            {
                if (!DAOWorker.HisWorkingShiftDAO.TruncateList(this.recentHisWorkingShifts))
                {
                    LogSystem.Warn("Rollback du lieu HisWorkingShift that bai, can kiem tra lai." + LogUtil.TraceData("recentHisWorkingShifts", this.recentHisWorkingShifts));
                }
				this.recentHisWorkingShifts = null;
            }
        }
    }
}
