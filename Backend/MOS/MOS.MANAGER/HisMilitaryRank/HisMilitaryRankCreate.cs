using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMilitaryRank
{
    partial class HisMilitaryRankCreate : BusinessBase
    {
		private HIS_MILITARY_RANK recentHisMilitaryRank;
		
        internal HisMilitaryRankCreate()
            : base()
        {

        }

        internal HisMilitaryRankCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MILITARY_RANK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMilitaryRankCheck checker = new HisMilitaryRankCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.MILITARY_RANK_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisMilitaryRankDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMilitaryRank_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMilitaryRank that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMilitaryRank = data;
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
            if (this.recentHisMilitaryRank != null)
            {
                if (!new HisMilitaryRankTruncate(param).Truncate(this.recentHisMilitaryRank))
                {
                    LogSystem.Warn("Rollback du lieu HisMilitaryRank that bai, can kiem tra lai." + LogUtil.TraceData("HisMilitaryRank", this.recentHisMilitaryRank));
                }
            }
        }
    }
}
