using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientClassify
{
    partial class HisPatientClassifyCreate : BusinessBase
    {
		private List<HIS_PATIENT_CLASSIFY> recentHisPatientClassifys = new List<HIS_PATIENT_CLASSIFY>();
		
        internal HisPatientClassifyCreate()
            : base()
        {

        }

        internal HisPatientClassifyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PATIENT_CLASSIFY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientClassifyCheck checker = new HisPatientClassifyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.PATIENT_CLASSIFY_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisPatientClassifyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientClassify_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPatientClassify that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPatientClassifys.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisPatientClassifys))
            {
                if (!DAOWorker.HisPatientClassifyDAO.TruncateList(this.recentHisPatientClassifys))
                {
                    LogSystem.Warn("Rollback du lieu HisPatientClassify that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPatientClassifys", this.recentHisPatientClassifys));
                }
				this.recentHisPatientClassifys = null;
            }
        }
    }
}
