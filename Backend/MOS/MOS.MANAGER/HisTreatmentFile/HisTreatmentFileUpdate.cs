using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTreatmentFile
{
    partial class HisTreatmentFileUpdate : BusinessBase
    {
		private List<HIS_TREATMENT_FILE> beforeUpdateHisTreatmentFiles = new List<HIS_TREATMENT_FILE>();
		
        internal HisTreatmentFileUpdate()
            : base()
        {

        }

        internal HisTreatmentFileUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TREATMENT_FILE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentFileCheck checker = new HisTreatmentFileCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyLength(data);
                HIS_TREATMENT_FILE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisTreatmentFileDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentFile_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatmentFile that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisTreatmentFiles.Add(raw);
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

        internal bool UpdateList(List<HIS_TREATMENT_FILE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentFileCheck checker = new HisTreatmentFileCheck(param);
                List<HIS_TREATMENT_FILE> listRaw = new List<HIS_TREATMENT_FILE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.VerifyLength(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisTreatmentFileDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentFile_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatmentFile that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisTreatmentFiles.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisTreatmentFiles))
            {
                if (!DAOWorker.HisTreatmentFileDAO.UpdateList(this.beforeUpdateHisTreatmentFiles))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatmentFile that bai, can kiem tra lai." + LogUtil.TraceData("HisTreatmentFiles", this.beforeUpdateHisTreatmentFiles));
                }
				this.beforeUpdateHisTreatmentFiles = null;
            }
        }
    }
}
