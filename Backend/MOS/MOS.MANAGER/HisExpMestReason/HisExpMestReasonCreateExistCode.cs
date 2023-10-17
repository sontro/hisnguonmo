using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestReason
{
    partial class HisExpMestReasonCreate : BusinessBase
    {
		private List<HIS_EXP_MEST_REASON> recentHisExpMestReasons = new List<HIS_EXP_MEST_REASON>();
		
        internal HisExpMestReasonCreate()
            : base()
        {

        }

        internal HisExpMestReasonCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EXP_MEST_REASON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestReasonCheck checker = new HisExpMestReasonCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.EXP_MEST_REASON_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisExpMestReasonDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestReason_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisExpMestReason that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisExpMestReasons.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisExpMestReasons))
            {
                if (!new HisExpMestReasonTruncate(param).TruncateList(this.recentHisExpMestReasons))
                {
                    LogSystem.Warn("Rollback du lieu HisExpMestReason that bai, can kiem tra lai." + LogUtil.TraceData("recentHisExpMestReasons", this.recentHisExpMestReasons));
                }
            }
        }
    }
}
