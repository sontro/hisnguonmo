using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreHandoverStt
{
    partial class HisHoreHandoverSttCreate : BusinessBase
    {
		private List<HIS_HORE_HANDOVER_STT> recentHisHoreHandoverStts = new List<HIS_HORE_HANDOVER_STT>();
		
        internal HisHoreHandoverSttCreate()
            : base()
        {

        }

        internal HisHoreHandoverSttCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_HORE_HANDOVER_STT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisHoreHandoverSttCheck checker = new HisHoreHandoverSttCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.HORE_HANDOVER_STT_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisHoreHandoverSttDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisHoreHandoverStt_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisHoreHandoverStt that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisHoreHandoverStts.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisHoreHandoverStts))
            {
                if (!DAOWorker.HisHoreHandoverSttDAO.TruncateList(this.recentHisHoreHandoverStts))
                {
                    LogSystem.Warn("Rollback du lieu HisHoreHandoverStt that bai, can kiem tra lai." + LogUtil.TraceData("recentHisHoreHandoverStts", this.recentHisHoreHandoverStts));
                }
				this.recentHisHoreHandoverStts = null;
            }
        }
    }
}
