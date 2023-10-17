using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationTime
{
    partial class HisRationTimeCreate : BusinessBase
    {
		private List<HIS_RATION_TIME> recentHisRationTimes = new List<HIS_RATION_TIME>();
		
        internal HisRationTimeCreate()
            : base()
        {

        }

        internal HisRationTimeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_RATION_TIME data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRationTimeCheck checker = new HisRationTimeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.RATION_TIME_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisRationTimeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationTime_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRationTime that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRationTimes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisRationTimes))
            {
                if (!DAOWorker.HisRationTimeDAO.TruncateList(this.recentHisRationTimes))
                {
                    LogSystem.Warn("Rollback du lieu HisRationTime that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRationTimes", this.recentHisRationTimes));
                }
				this.recentHisRationTimes = null;
            }
        }
    }
}
