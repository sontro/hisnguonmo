using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentType
{
    partial class HisTreatmentTypeCreate : BusinessBase
    {
		private HIS_TREATMENT_TYPE recentHisTreatmentType = new HIS_TREATMENT_TYPE();
		
        internal HisTreatmentTypeCreate()
            : base()
        {

        }

        internal HisTreatmentTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TREATMENT_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentTypeCheck checker = new HisTreatmentTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.TREATMENT_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisTreatmentTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTreatmentType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisTreatmentType = data;
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
            if (this.recentHisTreatmentType != null)
            {
                if (!new HisTreatmentTypeTruncate(param).Truncate(this.recentHisTreatmentType))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatmentType that bai, can kiem tra lai." + LogUtil.TraceData("HisTreatmentType", this.recentHisTreatmentType));
                }
            }
        }
    }
}
