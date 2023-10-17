using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpUserTemp
{
    partial class HisImpUserTempCreate : BusinessBase
    {
		private List<HIS_IMP_USER_TEMP> recentHisImpUserTemps = new List<HIS_IMP_USER_TEMP>();
		
        internal HisImpUserTempCreate()
            : base()
        {

        }

        internal HisImpUserTempCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_IMP_USER_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpUserTempCheck checker = new HisImpUserTempCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.IMP_USER_TEMP_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisImpUserTempDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpUserTemp_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisImpUserTemp that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisImpUserTemps.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisImpUserTemps))
            {
                if (!DAOWorker.HisImpUserTempDAO.TruncateList(this.recentHisImpUserTemps))
                {
                    LogSystem.Warn("Rollback du lieu HisImpUserTemp that bai, can kiem tra lai." + LogUtil.TraceData("recentHisImpUserTemps", this.recentHisImpUserTemps));
                }
				this.recentHisImpUserTemps = null;
            }
        }
    }
}
