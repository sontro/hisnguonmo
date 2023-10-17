using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisHivTreatment
{
    partial class HisHivTreatmentUpdate : BusinessBase
    {
		private List<HIS_HIV_TREATMENT> beforeUpdateHisHivTreatments = new List<HIS_HIV_TREATMENT>();
		
        internal HisHivTreatmentUpdate()
            : base()
        {

        }

        internal HisHivTreatmentUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_HIV_TREATMENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHivTreatmentCheck checker = new HisHivTreatmentCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_HIV_TREATMENT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.HIV_TREATMENT_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisHivTreatmentDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHivTreatment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHivTreatment that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisHivTreatments.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_HIV_TREATMENT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHivTreatmentCheck checker = new HisHivTreatmentCheck(param);
                List<HIS_HIV_TREATMENT> listRaw = new List<HIS_HIV_TREATMENT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.HIV_TREATMENT_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisHivTreatmentDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHivTreatment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHivTreatment that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisHivTreatments.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisHivTreatments))
            {
                if (!DAOWorker.HisHivTreatmentDAO.UpdateList(this.beforeUpdateHisHivTreatments))
                {
                    LogSystem.Warn("Rollback du lieu HisHivTreatment that bai, can kiem tra lai." + LogUtil.TraceData("HisHivTreatments", this.beforeUpdateHisHivTreatments));
                }
				this.beforeUpdateHisHivTreatments = null;
            }
        }
    }
}
