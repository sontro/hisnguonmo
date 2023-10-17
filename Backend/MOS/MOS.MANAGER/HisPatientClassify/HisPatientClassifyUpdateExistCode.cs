using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPatientClassify
{
    partial class HisPatientClassifyUpdate : BusinessBase
    {
		private List<HIS_PATIENT_CLASSIFY> beforeUpdateHisPatientClassifys = new List<HIS_PATIENT_CLASSIFY>();
		
        internal HisPatientClassifyUpdate()
            : base()
        {

        }

        internal HisPatientClassifyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PATIENT_CLASSIFY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientClassifyCheck checker = new HisPatientClassifyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PATIENT_CLASSIFY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.PATIENT_CLASSIFY_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisPatientClassifyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientClassify_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPatientClassify that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisPatientClassifys.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_PATIENT_CLASSIFY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPatientClassifyCheck checker = new HisPatientClassifyCheck(param);
                List<HIS_PATIENT_CLASSIFY> listRaw = new List<HIS_PATIENT_CLASSIFY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.PATIENT_CLASSIFY_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisPatientClassifyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientClassify_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPatientClassify that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisPatientClassifys.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPatientClassifys))
            {
                if (!DAOWorker.HisPatientClassifyDAO.UpdateList(this.beforeUpdateHisPatientClassifys))
                {
                    LogSystem.Warn("Rollback du lieu HisPatientClassify that bai, can kiem tra lai." + LogUtil.TraceData("HisPatientClassifys", this.beforeUpdateHisPatientClassifys));
                }
				this.beforeUpdateHisPatientClassifys = null;
            }
        }
    }
}
