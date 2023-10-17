using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisHospitalizeReason
{
    partial class HisHospitalizeReasonUpdate : BusinessBase
    {
		private List<HIS_HOSPITALIZE_REASON> beforeUpdateHisHospitalizeReasons = new List<HIS_HOSPITALIZE_REASON>();
		
        internal HisHospitalizeReasonUpdate()
            : base()
        {

        }

        internal HisHospitalizeReasonUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_HOSPITALIZE_REASON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHospitalizeReasonCheck checker = new HisHospitalizeReasonCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_HOSPITALIZE_REASON raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.HOSPITALIZE_REASON_CODE, data.ID);
                if (valid)
                {                    
					if (!DAOWorker.HisHospitalizeReasonDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHospitalizeReason_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHospitalizeReason that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisHospitalizeReasons.Add(raw);
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

        internal bool UpdateList(List<HIS_HOSPITALIZE_REASON> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisHospitalizeReasonCheck checker = new HisHospitalizeReasonCheck(param);
                List<HIS_HOSPITALIZE_REASON> listRaw = new List<HIS_HOSPITALIZE_REASON>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.HOSPITALIZE_REASON_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisHospitalizeReasonDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHospitalizeReason_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisHospitalizeReason that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisHospitalizeReasons.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisHospitalizeReasons))
            {
                if (!DAOWorker.HisHospitalizeReasonDAO.UpdateList(this.beforeUpdateHisHospitalizeReasons))
                {
                    LogSystem.Warn("Rollback du lieu HisHospitalizeReason that bai, can kiem tra lai." + LogUtil.TraceData("HisHospitalizeReasons", this.beforeUpdateHisHospitalizeReasons));
                }
				this.beforeUpdateHisHospitalizeReasons = null;
            }
        }
    }
}
