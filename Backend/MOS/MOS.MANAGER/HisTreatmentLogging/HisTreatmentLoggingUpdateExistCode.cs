using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentLogging
{
    partial class HisTreatmentLoggingUpdate : BusinessBase
    {
		private List<HIS_TREATMENT_LOGGING> beforeUpdateHisTreatmentLoggings = new List<HIS_TREATMENT_LOGGING>();
		
        internal HisTreatmentLoggingUpdate()
            : base()
        {

        }

        internal HisTreatmentLoggingUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TREATMENT_LOGGING data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentLoggingCheck checker = new HisTreatmentLoggingCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_TREATMENT_LOGGING raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.TREATMENT_LOGGING_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisTreatmentLoggings.Add(raw);
					if (!DAOWorker.HisTreatmentLoggingDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentLogging_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatmentLogging that bai." + LogUtil.TraceData("data", data));
                    }
                    
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

        internal bool UpdateList(List<HIS_TREATMENT_LOGGING> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentLoggingCheck checker = new HisTreatmentLoggingCheck(param);
                List<HIS_TREATMENT_LOGGING> listRaw = new List<HIS_TREATMENT_LOGGING>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.TREATMENT_LOGGING_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisTreatmentLoggings.AddRange(listRaw);
					if (!DAOWorker.HisTreatmentLoggingDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentLogging_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatmentLogging that bai." + LogUtil.TraceData("listData", listData));
                    }
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisTreatmentLoggings))
            {
                if (!new HisTreatmentLoggingUpdate(param).UpdateList(this.beforeUpdateHisTreatmentLoggings))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatmentLogging that bai, can kiem tra lai." + LogUtil.TraceData("HisTreatmentLoggings", this.beforeUpdateHisTreatmentLoggings));
                }
            }
        }
    }
}
