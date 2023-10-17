using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDrugIntervention
{
    partial class HisDrugInterventionUpdate : BusinessBase
    {
		private List<HIS_DRUG_INTERVENTION> beforeUpdateHisDrugInterventions = new List<HIS_DRUG_INTERVENTION>();
		
        internal HisDrugInterventionUpdate()
            : base()
        {

        }

        internal HisDrugInterventionUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_DRUG_INTERVENTION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDrugInterventionCheck checker = new HisDrugInterventionCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DRUG_INTERVENTION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisDrugInterventionDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDrugIntervention_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDrugIntervention that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisDrugInterventions.Add(raw);
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

        internal bool UpdateList(List<HIS_DRUG_INTERVENTION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDrugInterventionCheck checker = new HisDrugInterventionCheck(param);
                List<HIS_DRUG_INTERVENTION> listRaw = new List<HIS_DRUG_INTERVENTION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisDrugInterventionDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDrugIntervention_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDrugIntervention that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisDrugInterventions.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisDrugInterventions))
            {
                if (!DAOWorker.HisDrugInterventionDAO.UpdateList(this.beforeUpdateHisDrugInterventions))
                {
                    LogSystem.Warn("Rollback du lieu HisDrugIntervention that bai, can kiem tra lai." + LogUtil.TraceData("HisDrugInterventions", this.beforeUpdateHisDrugInterventions));
                }
				this.beforeUpdateHisDrugInterventions = null;
            }
        }
    }
}
