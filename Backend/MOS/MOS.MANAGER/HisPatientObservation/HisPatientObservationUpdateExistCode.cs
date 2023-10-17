using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPatientObservation
{
    partial class HisPatientObservationUpdate : BusinessBase
    {
		private List<HIS_PATIENT_OBSERVATION> beforeUpdateHisPatientObservations = new List<HIS_PATIENT_OBSERVATION>();
		
        internal HisPatientObservationUpdate()
            : base()
        {

        }

        internal HisPatientObservationUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PATIENT_OBSERVATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientObservationCheck checker = new HisPatientObservationCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PATIENT_OBSERVATION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.PATIENT_OBSERVATION_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisPatientObservationDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientObservation_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPatientObservation that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisPatientObservations.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_PATIENT_OBSERVATION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPatientObservationCheck checker = new HisPatientObservationCheck(param);
                List<HIS_PATIENT_OBSERVATION> listRaw = new List<HIS_PATIENT_OBSERVATION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.PATIENT_OBSERVATION_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisPatientObservationDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientObservation_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPatientObservation that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisPatientObservations.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPatientObservations))
            {
                if (!DAOWorker.HisPatientObservationDAO.UpdateList(this.beforeUpdateHisPatientObservations))
                {
                    LogSystem.Warn("Rollback du lieu HisPatientObservation that bai, can kiem tra lai." + LogUtil.TraceData("HisPatientObservations", this.beforeUpdateHisPatientObservations));
                }
				this.beforeUpdateHisPatientObservations = null;
            }
        }
    }
}
