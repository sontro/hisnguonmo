using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccineType
{
    partial class HisVaccineTypeCreate : BusinessBase
    {
		private List<HIS_VACCINE_TYPE> recentHisVaccineTypes = new List<HIS_VACCINE_TYPE>();
		
        internal HisVaccineTypeCreate()
            : base()
        {

        }

        internal HisVaccineTypeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_VACCINE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccineTypeCheck checker = new HisVaccineTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.VACCINE_TYPE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisVaccineTypeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccineType_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccineType that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisVaccineTypes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisVaccineTypes))
            {
                if (!DAOWorker.HisVaccineTypeDAO.TruncateList(this.recentHisVaccineTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccineType that bai, can kiem tra lai." + LogUtil.TraceData("recentHisVaccineTypes", this.recentHisVaccineTypes));
                }
				this.recentHisVaccineTypes = null;
            }
        }
    }
}
