using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentEndTypeExt
{
    partial class HisTreatmentEndTypeExtCreate : BusinessBase
    {
		private List<HIS_TREATMENT_END_TYPE_EXT> recentHisTreatmentEndTypeExts = new List<HIS_TREATMENT_END_TYPE_EXT>();
		
        internal HisTreatmentEndTypeExtCreate()
            : base()
        {

        }

        internal HisTreatmentEndTypeExtCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TREATMENT_END_TYPE_EXT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentEndTypeExtCheck checker = new HisTreatmentEndTypeExtCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.TREATMENT_END_TYPE_EXT_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisTreatmentEndTypeExtDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentEndTypeExt_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTreatmentEndTypeExt that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisTreatmentEndTypeExts.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisTreatmentEndTypeExts))
            {
                if (!DAOWorker.HisTreatmentEndTypeExtDAO.TruncateList(this.recentHisTreatmentEndTypeExts))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatmentEndTypeExt that bai, can kiem tra lai." + LogUtil.TraceData("recentHisTreatmentEndTypeExts", this.recentHisTreatmentEndTypeExts));
                }
				this.recentHisTreatmentEndTypeExts = null;
            }
        }
    }
}
