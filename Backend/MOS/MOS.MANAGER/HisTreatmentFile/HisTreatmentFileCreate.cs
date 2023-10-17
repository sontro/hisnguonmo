using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentFile
{
    partial class HisTreatmentFileCreate : BusinessBase
    {
		private List<HIS_TREATMENT_FILE> recentHisTreatmentFiles = new List<HIS_TREATMENT_FILE>();
		
        internal HisTreatmentFileCreate()
            : base()
        {

        }

        internal HisTreatmentFileCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TREATMENT_FILE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentFileCheck checker = new HisTreatmentFileCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyLength(data);
                if (valid)
                {
					if (!DAOWorker.HisTreatmentFileDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentFile_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTreatmentFile that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisTreatmentFiles.Add(data);
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
		
		internal bool CreateList(List<HIS_TREATMENT_FILE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentFileCheck checker = new HisTreatmentFileCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.VerifyLength(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisTreatmentFileDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentFile_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTreatmentFile that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisTreatmentFiles.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisTreatmentFiles))
            {
                if (!DAOWorker.HisTreatmentFileDAO.TruncateList(this.recentHisTreatmentFiles))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatmentFile that bai, can kiem tra lai." + LogUtil.TraceData("recentHisTreatmentFiles", this.recentHisTreatmentFiles));
                }
				this.recentHisTreatmentFiles = null;
            }
        }
    }
}
