using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisOtherPaySource
{
    partial class HisOtherPaySourceCreate : BusinessBase
    {
		private List<HIS_OTHER_PAY_SOURCE> recentHisOtherPaySources = new List<HIS_OTHER_PAY_SOURCE>();
		
        internal HisOtherPaySourceCreate()
            : base()
        {

        }

        internal HisOtherPaySourceCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_OTHER_PAY_SOURCE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisOtherPaySourceCheck checker = new HisOtherPaySourceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.OTHER_PAY_SOURCE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisOtherPaySourceDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisOtherPaySource_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisOtherPaySource that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisOtherPaySources.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisOtherPaySources))
            {
                if (!DAOWorker.HisOtherPaySourceDAO.TruncateList(this.recentHisOtherPaySources))
                {
                    LogSystem.Warn("Rollback du lieu HisOtherPaySource that bai, can kiem tra lai." + LogUtil.TraceData("recentHisOtherPaySources", this.recentHisOtherPaySources));
                }
				this.recentHisOtherPaySources = null;
            }
        }
    }
}
