using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceCondition
{
    partial class HisServiceConditionCreate : BusinessBase
    {
        private List<HIS_SERVICE_CONDITION> recentHisServiceConditions = new List<HIS_SERVICE_CONDITION>();

        internal HisServiceConditionCreate()
            : base()
        {

        }

        internal HisServiceConditionCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERVICE_CONDITION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceConditionCheck checker = new HisServiceConditionCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.SERVICE_CONDITION_CODE, null);
                if (valid)
                {
                    if (!DAOWorker.HisServiceConditionDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceCondition_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceCondition that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisServiceConditions.Add(data);
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

        internal bool CreateList(List<HIS_SERVICE_CONDITION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceConditionCheck checker = new HisServiceConditionCheck(param);
                foreach (HIS_SERVICE_CONDITION data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.SERVICE_CONDITION_CODE, null);
                }
                if (valid)
                {
                    if (!DAOWorker.HisServiceConditionDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceCondition_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceCondition that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisServiceConditions.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisServiceConditions))
            {
                if (!DAOWorker.HisServiceConditionDAO.TruncateList(this.recentHisServiceConditions))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceCondition that bai, can kiem tra lai." + LogUtil.TraceData("recentHisServiceConditions", this.recentHisServiceConditions));
                }
                this.recentHisServiceConditions = null;
            }
        }
    }
}
