using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodBlood
{
    partial class HisMestPeriodBloodCreate : BusinessBase
    {
		private List<HIS_MEST_PERIOD_BLOOD> recentHisMestPeriodBloods = new List<HIS_MEST_PERIOD_BLOOD>();
		
        internal HisMestPeriodBloodCreate()
            : base()
        {

        }

        internal HisMestPeriodBloodCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEST_PERIOD_BLOOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestPeriodBloodCheck checker = new HisMestPeriodBloodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.MEST_PERIOD_BLOOD_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisMestPeriodBloodDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestPeriodBlood_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMestPeriodBlood that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMestPeriodBloods.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisMestPeriodBloods))
            {
                if (!new HisMestPeriodBloodTruncate(param).TruncateList(this.recentHisMestPeriodBloods))
                {
                    LogSystem.Warn("Rollback du lieu HisMestPeriodBlood that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMestPeriodBloods", this.recentHisMestPeriodBloods));
                }
            }
        }
    }
}
