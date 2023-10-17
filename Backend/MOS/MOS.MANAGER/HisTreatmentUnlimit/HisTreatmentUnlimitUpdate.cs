using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentUnlimit
{
    partial class HisTreatmentUnlimitUpdate : BusinessBase
    {
		private List<HIS_TREATMENT_UNLIMIT> beforeUpdateHisTreatmentUnlimits = new List<HIS_TREATMENT_UNLIMIT>();
		
        internal HisTreatmentUnlimitUpdate()
            : base()
        {

        }

        internal HisTreatmentUnlimitUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TREATMENT_UNLIMIT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentUnlimitCheck checker = new HisTreatmentUnlimitCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_TREATMENT_UNLIMIT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisTreatmentUnlimitDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentUnlimit_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatmentUnlimit that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisTreatmentUnlimits.Add(raw);
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

        internal bool UpdateList(List<HIS_TREATMENT_UNLIMIT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentUnlimitCheck checker = new HisTreatmentUnlimitCheck(param);
                List<HIS_TREATMENT_UNLIMIT> listRaw = new List<HIS_TREATMENT_UNLIMIT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisTreatmentUnlimitDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentUnlimit_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatmentUnlimit that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisTreatmentUnlimits.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisTreatmentUnlimits))
            {
                if (!DAOWorker.HisTreatmentUnlimitDAO.UpdateList(this.beforeUpdateHisTreatmentUnlimits))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatmentUnlimit that bai, can kiem tra lai." + LogUtil.TraceData("HisTreatmentUnlimits", this.beforeUpdateHisTreatmentUnlimits));
                }
				this.beforeUpdateHisTreatmentUnlimits = null;
            }
        }
    }
}
