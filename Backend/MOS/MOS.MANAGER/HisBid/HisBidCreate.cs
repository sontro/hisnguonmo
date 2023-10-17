using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBid
{
    partial class HisBidCreate : BusinessBase
    {
		private List<HIS_BID> recentHisBids = new List<HIS_BID>();
		
        internal HisBidCreate()
            : base()
        {

        }

        internal HisBidCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BID data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBidCheck checker = new HisBidCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisBidDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBid_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBid that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBids.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisBids))
            {
                if (!new HisBidTruncate(param).TruncateList(this.recentHisBids))
                {
                    LogSystem.Warn("Rollback du lieu HisBid that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBids", this.recentHisBids));
                }
            }
        }
    }
}
