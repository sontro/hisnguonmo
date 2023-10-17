using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmergencyWtime
{
    partial class HisEmergencyWtimeCreate : BusinessBase
    {
        private List<HIS_EMERGENCY_WTIME> recentHisEmergencyWtimes = new List<HIS_EMERGENCY_WTIME>();

        internal HisEmergencyWtimeCreate()
            : base()
        {

        }

        internal HisEmergencyWtimeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EMERGENCY_WTIME data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmergencyWtimeCheck checker = new HisEmergencyWtimeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisEmergencyWtimeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmergencyWtime_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEmergencyWtime that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisEmergencyWtimes.Add(data);
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

        internal bool CreateList(List<HIS_EMERGENCY_WTIME> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEmergencyWtimeCheck checker = new HisEmergencyWtimeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisEmergencyWtimeDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmergencyWtime_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEmergencyWtime that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisEmergencyWtimes.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisEmergencyWtimes))
            {
                if (!new HisEmergencyWtimeTruncate(param).TruncateList(this.recentHisEmergencyWtimes))
                {
                    LogSystem.Warn("Rollback du lieu HisEmergencyWtime that bai, can kiem tra lai." + LogUtil.TraceData("recentHisEmergencyWtimes", this.recentHisEmergencyWtimes));
                }
            }
        }
    }
}
