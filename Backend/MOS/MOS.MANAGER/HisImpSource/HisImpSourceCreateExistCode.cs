using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpSource
{
    partial class HisImpSourceCreate : BusinessBase
    {
		private List<HIS_IMP_SOURCE> recentHisImpSources = new List<HIS_IMP_SOURCE>();
		
        internal HisImpSourceCreate()
            : base()
        {

        }

        internal HisImpSourceCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_IMP_SOURCE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpSourceCheck checker = new HisImpSourceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.IMP_SOURCE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisImpSourceDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpSource_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisImpSource that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisImpSources.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisImpSources))
            {
                if (!new HisImpSourceTruncate(param).TruncateList(this.recentHisImpSources))
                {
                    LogSystem.Warn("Rollback du lieu HisImpSource that bai, can kiem tra lai." + LogUtil.TraceData("recentHisImpSources", this.recentHisImpSources));
                }
            }
        }
    }
}
