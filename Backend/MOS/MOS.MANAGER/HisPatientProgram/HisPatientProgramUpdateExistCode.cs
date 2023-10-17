using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPatientProgram
{
    partial class HisPatientProgramUpdate : BusinessBase
    {
		private List<HIS_PATIENT_PROGRAM> beforeUpdateHisPatientPrograms = new List<HIS_PATIENT_PROGRAM>();
		
        internal HisPatientProgramUpdate()
            : base()
        {

        }

        internal HisPatientProgramUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PATIENT_PROGRAM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPatientProgramCheck checker = new HisPatientProgramCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PATIENT_PROGRAM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.PATIENT_PROGRAM_CODE, data.ID);
                valid = valid && checker.IsNotExist(data);
                if (valid)
                {
                    this.beforeUpdateHisPatientPrograms.Add(raw);
					if (!DAOWorker.HisPatientProgramDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientProgram_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPatientProgram that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_PATIENT_PROGRAM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPatientProgramCheck checker = new HisPatientProgramCheck(param);
                List<HIS_PATIENT_PROGRAM> listRaw = new List<HIS_PATIENT_PROGRAM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.PATIENT_PROGRAM_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisPatientPrograms.AddRange(listRaw);
					if (!DAOWorker.HisPatientProgramDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatientProgram_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPatientProgram that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPatientPrograms))
            {
                if (!new HisPatientProgramUpdate(param).UpdateList(this.beforeUpdateHisPatientPrograms))
                {
                    LogSystem.Warn("Rollback du lieu HisPatientProgram that bai, can kiem tra lai." + LogUtil.TraceData("HisPatientPrograms", this.beforeUpdateHisPatientPrograms));
                }
            }
        }
    }
}
