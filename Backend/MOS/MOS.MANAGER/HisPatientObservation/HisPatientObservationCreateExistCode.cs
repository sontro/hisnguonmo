using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientObservation
{
    partial class HisPatientObservationCreate : BusinessBase
    {
		private List<HIS_PATIENT_OBSERVATION> recentHisPatientObservations = new List<HIS_PATIENT_OBSERVATION>();
		
        internal HisPatientObservationCreate()
            : base()
        {

        }

        internal HisPatientObservationCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PATIENT_OBSERVATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientObservationCheck checker = new HisPatientObservationCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.PATIENT_OBSERVATION_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisPatientObservationDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientObservation_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPatientObservation that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPatientObservations.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisPatientObservations))
            {
                if (!DAOWorker.HisPatientObservationDAO.TruncateList(this.recentHisPatientObservations))
                {
                    LogSystem.Warn("Rollback du lieu HisPatientObservation that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPatientObservations", this.recentHisPatientObservations));
                }
				this.recentHisPatientObservations = null;
            }
        }
    }
}
