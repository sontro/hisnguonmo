using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNextTreaIntr
{
    partial class HisNextTreaIntrCreate : BusinessBase
    {
		private List<HIS_NEXT_TREA_INTR> recentHisNextTreaIntrs = new List<HIS_NEXT_TREA_INTR>();
		
        internal HisNextTreaIntrCreate()
            : base()
        {

        }

        internal HisNextTreaIntrCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_NEXT_TREA_INTR data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisNextTreaIntrCheck checker = new HisNextTreaIntrCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.NEXT_TREA_INTR_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisNextTreaIntrDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisNextTreaIntr_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisNextTreaIntr that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisNextTreaIntrs.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisNextTreaIntrs))
            {
                if (!DAOWorker.HisNextTreaIntrDAO.TruncateList(this.recentHisNextTreaIntrs))
                {
                    LogSystem.Warn("Rollback du lieu HisNextTreaIntr that bai, can kiem tra lai." + LogUtil.TraceData("recentHisNextTreaIntrs", this.recentHisNextTreaIntrs));
                }
				this.recentHisNextTreaIntrs = null;
            }
        }
    }
}
