using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientCase
{
    partial class HisPatientCaseCreate : BusinessBase
    {
		private List<HIS_PATIENT_CASE> recentHisPatientCases = new List<HIS_PATIENT_CASE>();
		
        internal HisPatientCaseCreate()
            : base()
        {

        }

        internal HisPatientCaseCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PATIENT_CASE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientCaseCheck checker = new HisPatientCaseCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.PATIENT_CASE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisPatientCaseDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientCase_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPatientCase that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPatientCases.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisPatientCases))
            {
                if (!DAOWorker.HisPatientCaseDAO.TruncateList(this.recentHisPatientCases))
                {
                    LogSystem.Warn("Rollback du lieu HisPatientCase that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPatientCases", this.recentHisPatientCases));
                }
				this.recentHisPatientCases = null;
            }
        }
    }
}
