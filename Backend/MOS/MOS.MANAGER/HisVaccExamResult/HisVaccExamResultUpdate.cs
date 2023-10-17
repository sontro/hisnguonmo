using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccExamResult
{
    partial class HisVaccExamResultUpdate : BusinessBase
    {
		private List<HIS_VACC_EXAM_RESULT> beforeUpdateHisVaccExamResults = new List<HIS_VACC_EXAM_RESULT>();
		
        internal HisVaccExamResultUpdate()
            : base()
        {

        }

        internal HisVaccExamResultUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_VACC_EXAM_RESULT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccExamResultCheck checker = new HisVaccExamResultCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_VACC_EXAM_RESULT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisVaccExamResultDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccExamResult_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccExamResult that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisVaccExamResults.Add(raw);
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

        internal bool UpdateList(List<HIS_VACC_EXAM_RESULT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccExamResultCheck checker = new HisVaccExamResultCheck(param);
                List<HIS_VACC_EXAM_RESULT> listRaw = new List<HIS_VACC_EXAM_RESULT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisVaccExamResultDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccExamResult_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccExamResult that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisVaccExamResults.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisVaccExamResults))
            {
                if (!DAOWorker.HisVaccExamResultDAO.UpdateList(this.beforeUpdateHisVaccExamResults))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccExamResult that bai, can kiem tra lai." + LogUtil.TraceData("HisVaccExamResults", this.beforeUpdateHisVaccExamResults));
                }
				this.beforeUpdateHisVaccExamResults = null;
            }
        }
    }
}
