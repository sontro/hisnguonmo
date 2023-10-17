using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCoTreatment
{
    partial class HisCoTreatmentUpdate : BusinessBase
    {
		private List<HIS_CO_TREATMENT> beforeUpdateHisCoTreatments = new List<HIS_CO_TREATMENT>();
		
        internal HisCoTreatmentUpdate()
            : base()
        {

        }

        internal HisCoTreatmentUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_CO_TREATMENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCoTreatmentCheck checker = new HisCoTreatmentCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_CO_TREATMENT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.CheckTime(data);
                if (valid)
                {                    
					if (!DAOWorker.HisCoTreatmentDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCoTreatment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCoTreatment that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisCoTreatments.Add(raw);
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

        internal bool Update(HIS_CO_TREATMENT data, HIS_CO_TREATMENT beforeUpdate)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCoTreatmentCheck checker = new HisCoTreatmentCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsUnLock(beforeUpdate);
                if (valid)
                {
                    if (!DAOWorker.HisCoTreatmentDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCoTreatment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCoTreatment that bai." + LogUtil.TraceData("data", data));
                    }
                    this.beforeUpdateHisCoTreatments.Add(beforeUpdate);
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

        internal bool UpdateList(List<HIS_CO_TREATMENT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCoTreatmentCheck checker = new HisCoTreatmentCheck(param);
                List<HIS_CO_TREATMENT> listRaw = new List<HIS_CO_TREATMENT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisCoTreatmentDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCoTreatment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCoTreatment that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisCoTreatments.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisCoTreatments))
            {
                if (!DAOWorker.HisCoTreatmentDAO.UpdateList(this.beforeUpdateHisCoTreatments))
                {
                    LogSystem.Warn("Rollback du lieu HisCoTreatment that bai, can kiem tra lai." + LogUtil.TraceData("HisCoTreatments", this.beforeUpdateHisCoTreatments));
                }
				this.beforeUpdateHisCoTreatments = null;
            }
        }
    }
}
