using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpUserTempDt
{
    partial class HisImpUserTempDtCreate : BusinessBase
    {
		private List<HIS_IMP_USER_TEMP_DT> recentHisImpUserTempDts = new List<HIS_IMP_USER_TEMP_DT>();
		
        internal HisImpUserTempDtCreate()
            : base()
        {

        }

        internal HisImpUserTempDtCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_IMP_USER_TEMP_DT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpUserTempDtCheck checker = new HisImpUserTempDtCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.IMP_USER_TEMP_DT_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisImpUserTempDtDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpUserTempDt_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisImpUserTempDt that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisImpUserTempDts.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisImpUserTempDts))
            {
                if (!DAOWorker.HisImpUserTempDtDAO.TruncateList(this.recentHisImpUserTempDts))
                {
                    LogSystem.Warn("Rollback du lieu HisImpUserTempDt that bai, can kiem tra lai." + LogUtil.TraceData("recentHisImpUserTempDts", this.recentHisImpUserTempDts));
                }
				this.recentHisImpUserTempDts = null;
            }
        }
    }
}
