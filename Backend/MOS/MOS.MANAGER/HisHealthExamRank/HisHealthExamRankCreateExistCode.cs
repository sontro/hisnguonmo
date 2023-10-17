using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHealthExamRank
{
    partial class HisHealthExamRankCreate : BusinessBase
    {
		private List<HIS_HEALTH_EXAM_RANK> recentHisHealthExamRanks = new List<HIS_HEALTH_EXAM_RANK>();
		
        internal HisHealthExamRankCreate()
            : base()
        {

        }

        internal HisHealthExamRankCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_HEALTH_EXAM_RANK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHealthExamRankCheck checker = new HisHealthExamRankCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.HEALTH_EXAM_RANK_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisHealthExamRankDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHealthExamRank_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisHealthExamRank that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisHealthExamRanks.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisHealthExamRanks))
            {
                if (!DAOWorker.HisHealthExamRankDAO.TruncateList(this.recentHisHealthExamRanks))
                {
                    LogSystem.Warn("Rollback du lieu HisHealthExamRank that bai, can kiem tra lai." + LogUtil.TraceData("recentHisHealthExamRanks", this.recentHisHealthExamRanks));
                }
				this.recentHisHealthExamRanks = null;
            }
        }
    }
}
