using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPatientTypeSub
{
    partial class HisPatientTypeSubUpdate : BusinessBase
    {
		private List<HIS_PATIENT_TYPE_SUB> beforeUpdateHisPatientTypeSubs = new List<HIS_PATIENT_TYPE_SUB>();
		
        internal HisPatientTypeSubUpdate()
            : base()
        {

        }

        internal HisPatientTypeSubUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PATIENT_TYPE_SUB data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientTypeSubCheck checker = new HisPatientTypeSubCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PATIENT_TYPE_SUB raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisPatientTypeSubs.Add(raw);
					if (!DAOWorker.HisPatientTypeSubDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientTypeSub_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPatientTypeSub that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_PATIENT_TYPE_SUB> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPatientTypeSubCheck checker = new HisPatientTypeSubCheck(param);
                List<HIS_PATIENT_TYPE_SUB> listRaw = new List<HIS_PATIENT_TYPE_SUB>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisPatientTypeSubs.AddRange(listRaw);
					if (!DAOWorker.HisPatientTypeSubDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientTypeSub_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPatientTypeSub that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPatientTypeSubs))
            {
                if (!new HisPatientTypeSubUpdate(param).UpdateList(this.beforeUpdateHisPatientTypeSubs))
                {
                    LogSystem.Warn("Rollback du lieu HisPatientTypeSub that bai, can kiem tra lai." + LogUtil.TraceData("HisPatientTypeSubs", this.beforeUpdateHisPatientTypeSubs));
                }
            }
        }
    }
}
