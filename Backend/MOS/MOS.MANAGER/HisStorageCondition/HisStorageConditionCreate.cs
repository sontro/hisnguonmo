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
		
		internal bool CreateList(List<HIS_STORAGE_CONDITION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisStorageConditionCheck checker = new HisStorageConditionCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisStorageConditionDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisStorageCondition_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisStorageCondition that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisStorageConditions.AddRange(listData);
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
