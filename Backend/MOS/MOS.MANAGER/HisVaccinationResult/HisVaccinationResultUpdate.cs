using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccinationResult
{
    partial class HisVaccinationResultUpdate : BusinessBase
    {
		private List<HIS_VACCINATION_RESULT> beforeUpdateHisVaccinationResults = new List<HIS_VACCINATION_RESULT>();
		
        internal HisVaccinationResultUpdate()
            : base()
        {

        }

        internal HisVaccinationResultUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_VACCINATION_RESULT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationResultCheck checker = new HisVaccinationResultCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_VACCINATION_RESULT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisVaccinationResultDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationResult_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccinationResult that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisVaccinationResults.Add(raw);
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

        internal bool UpdateList(List<HIS_VACCINATION_RESULT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccinationResultCheck checker = new HisVaccinationResultCheck(param);
                List<HIS_VACCINATION_RESULT> listRaw = new List<HIS_VACCINATION_RESULT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisVaccinationResultDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationResult_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccinationResult that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisVaccinationResults.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisVaccinationResults))
            {
                if (!DAOWorker.HisVaccinationResultDAO.UpdateList(this.beforeUpdateHisVaccinationResults))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccinationResult that bai, can kiem tra lai." + LogUtil.TraceData("HisVaccinationResults", this.beforeUpdateHisVaccinationResults));
                }
				this.beforeUpdateHisVaccinationResults = null;
            }
        }
    }
}
