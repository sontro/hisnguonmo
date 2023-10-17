using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentUnlimit
{
    partial class HisTreatmentUnlimitCreate : BusinessBase
    {
		private List<HIS_TREATMENT_UNLIMIT> recentHisTreatmentUnlimits = new List<HIS_TREATMENT_UNLIMIT>();
		
        internal HisTreatmentUnlimitCreate()
            : base()
        {

        }

        internal HisTreatmentUnlimitCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TREATMENT_UNLIMIT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentUnlimitCheck checker = new HisTreatmentUnlimitCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisTreatmentUnlimitDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentUnlimit_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTreatmentUnlimit that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisTreatmentUnlimits.Add(data);
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
		
		internal bool CreateList(List<HIS_TREATMENT_UNLIMIT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTreatmentUnlimitCheck checker = new HisTreatmentUnlimitCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisTreatmentUnlimitDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentUnlimit_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTreatmentUnlimit that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisTreatmentUnlimits.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisTreatmentUnlimits))
            {
                if (!DAOWorker.HisTreatmentUnlimitDAO.TruncateList(this.recentHisTreatmentUnlimits))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatmentUnlimit that bai, can kiem tra lai." + LogUtil.TraceData("recentHisTreatmentUnlimits", this.recentHisTreatmentUnlimits));
                }
				this.recentHisTreatmentUnlimits = null;
            }
        }
    }
}
