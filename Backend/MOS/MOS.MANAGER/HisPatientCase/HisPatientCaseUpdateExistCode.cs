using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPatientCase
{
    partial class HisPatientCaseUpdate : BusinessBase
    {
		private List<HIS_PATIENT_CASE> beforeUpdateHisPatientCases = new List<HIS_PATIENT_CASE>();
		
        internal HisPatientCaseUpdate()
            : base()
        {

        }

        internal HisPatientCaseUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PATIENT_CASE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientCaseCheck checker = new HisPatientCaseCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PATIENT_CASE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.PATIENT_CASE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisPatientCaseDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientCase_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPatientCase that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisPatientCases.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_PATIENT_CASE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPatientCaseCheck checker = new HisPatientCaseCheck(param);
                List<HIS_PATIENT_CASE> listRaw = new List<HIS_PATIENT_CASE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.PATIENT_CASE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisPatientCaseDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientCase_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPatientCase that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisPatientCases.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPatientCases))
            {
                if (!DAOWorker.HisPatientCaseDAO.UpdateList(this.beforeUpdateHisPatientCases))
                {
                    LogSystem.Warn("Rollback du lieu HisPatientCase that bai, can kiem tra lai." + LogUtil.TraceData("HisPatientCases", this.beforeUpdateHisPatientCases));
                }
				this.beforeUpdateHisPatientCases = null;
            }
        }
    }
}
