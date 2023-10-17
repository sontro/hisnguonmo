using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStentConclude
{
    partial class HisStentConcludeCreate : BusinessBase
    {
		private List<HIS_STENT_CONCLUDE> recentHisStentConcludes = new List<HIS_STENT_CONCLUDE>();
		
        internal HisStentConcludeCreate()
            : base()
        {

        }

        internal HisStentConcludeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_STENT_CONCLUDE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisStentConcludeCheck checker = new HisStentConcludeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.STENT_CONCLUDE_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisStentConcludeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisStentConclude_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisStentConclude that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisStentConcludes.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisStentConcludes))
            {
                if (!DAOWorker.HisStentConcludeDAO.TruncateList(this.recentHisStentConcludes))
                {
                    LogSystem.Warn("Rollback du lieu HisStentConclude that bai, can kiem tra lai." + LogUtil.TraceData("recentHisStentConcludes", this.recentHisStentConcludes));
                }
				this.recentHisStentConcludes = null;
            }
        }
    }
}
