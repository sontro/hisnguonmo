using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatientProgram
{
    partial class HisPatientProgramCreate : BusinessBase
    {
		private List<HIS_PATIENT_PROGRAM> recentHisPatientPrograms = new List<HIS_PATIENT_PROGRAM>();
		
        internal HisPatientProgramCreate()
            : base()
        {

        }

        internal HisPatientProgramCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool CreateNoCheckExists(HIS_PATIENT_PROGRAM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientProgramCheck checker = new HisPatientProgramCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisPatientProgramDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientProgram_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPatientProgram that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPatientPrograms.Add(data);
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

        internal bool Create(HIS_PATIENT_PROGRAM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientProgramCheck checker = new HisPatientProgramCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.PATIENT_PROGRAM_CODE, null);
                valid = valid && checker.IsNotExist(data);
                if (valid)
                {
					if (!DAOWorker.HisPatientProgramDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientProgram_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPatientProgram that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisPatientPrograms.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisPatientPrograms))
            {
                if (!DAOWorker.HisPatientProgramDAO.TruncateList(this.recentHisPatientPrograms))
                {
                    LogSystem.Warn("Rollback du lieu HisPatientProgram that bai, can kiem tra lai." + LogUtil.TraceData("recentHisPatientPrograms", this.recentHisPatientPrograms));
                }
            }
        }
    }
}
