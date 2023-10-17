using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStorageCondition
{
    partial class HisStorageConditionCreate : BusinessBase
    {
		private List<HIS_STORAGE_CONDITION> recentHisStorageConditions = new List<HIS_STORAGE_CONDITION>();
		
        internal HisStorageConditionCreate()
            : base()
        {

        }

        internal HisStorageConditionCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_STORAGE_CONDITION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisStorageConditionCheck checker = new HisStorageConditionCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.STORAGE_CONDITION_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisStorageConditionDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisStorageCondition_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisStorageCondition that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisStorageConditions.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisStorageConditions))
            {
                if (!DAOWorker.HisStorageConditionDAO.TruncateList(this.recentHisStorageConditions))
                {
                    LogSystem.Warn("Rollback du lieu HisStorageCondition that bai, can kiem tra lai." + LogUtil.TraceData("recentHisStorageConditions", this.recentHisStorageConditions));
                }
				this.recentHisStorageConditions = null;
            }
        }
    }
}
