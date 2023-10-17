using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientTypeSub
{
    partial class HisPatientTypeSubCreate : BusinessBase
    {
		private List<HIS_PATIENT_TYPE_SUB> recentHisPatientTypeSubs = new List<HIS_PATIENT_TYPE_SUB>();
		
        internal HisPatientTypeSubCreate()
            : base()
        {

        }

        internal HisPatientTypeSubCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_PATIENT_TYPE_SUB data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientTypeSubCheck checker = new HisPatientTypeSubCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisPatientTypeSubDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientTypeSub_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPatientTypeSub that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPatientTypeSubs.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisPatientTypeSubs))
            {
                if (!new HisPatientTypeSubTruncate(param).TruncateList(this.recentHisPatientTypeSubs))
                {
                    LogSystem.Warn("Rollback du lieu HisPatientTypeSub that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPatientTypeSubs", this.recentHisPatientTypeSubs));
                }
            }
        }
    }
}
