using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisHealthExamRank
{
    partial class HisHealthExamRankUpdate : BusinessBase
    {
		private List<HIS_HEALTH_EXAM_RANK> beforeUpdateHisHealthExamRanks = new List<HIS_HEALTH_EXAM_RANK>();
		
        internal HisHealthExamRankUpdate()
            : base()
        {

        }

        internal HisHealthExamRankUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_HEALTH_EXAM_RANK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHealthExamRankCheck checker = new HisHealthExamRankCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_HEALTH_EXAM_RANK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.HEALTH_EXAM_RANK_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisHealthExamRankDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHealthExamRank_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHealthExamRank that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisHealthExamRanks.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_HEALTH_EXAM_RANK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHealthExamRankCheck checker = new HisHealthExamRankCheck(param);
                List<HIS_HEALTH_EXAM_RANK> listRaw = new List<HIS_HEALTH_EXAM_RANK>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.HEALTH_EXAM_RANK_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisHealthExamRankDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHealthExamRank_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHealthExamRank that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisHealthExamRanks.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisHealthExamRanks))
            {
                if (!DAOWorker.HisHealthExamRankDAO.UpdateList(this.beforeUpdateHisHealthExamRanks))
                {
                    LogSystem.Warn("Rollback du lieu HisHealthExamRank that bai, can kiem tra lai." + LogUtil.TraceData("HisHealthExamRanks", this.beforeUpdateHisHealthExamRanks));
                }
				this.beforeUpdateHisHealthExamRanks = null;
            }
        }
    }
}
