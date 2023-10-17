using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSpeciality
{
    partial class HisSpecialityCreate : BusinessBase
    {
		private List<HIS_SPECIALITY> recentHisSpecialitys = new List<HIS_SPECIALITY>();
		
        internal HisSpecialityCreate()
            : base()
        {

        }

        internal HisSpecialityCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SPECIALITY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSpecialityCheck checker = new HisSpecialityCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.SPECIALITY_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisSpecialityDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSpeciality_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSpeciality that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSpecialitys.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisSpecialitys))
            {
                if (!DAOWorker.HisSpecialityDAO.TruncateList(this.recentHisSpecialitys))
                {
                    LogSystem.Warn("Rollback du lieu HisSpeciality that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSpecialitys", this.recentHisSpecialitys));
                }
				this.recentHisSpecialitys = null;
            }
        }
    }
}
