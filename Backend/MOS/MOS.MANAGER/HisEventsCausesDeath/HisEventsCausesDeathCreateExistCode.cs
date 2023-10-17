using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEventsCausesDeath
{
    partial class HisEventsCausesDeathCreate : BusinessBase
    {
		private List<HIS_EVENTS_CAUSES_DEATH> recentHisEventsCausesDeaths = new List<HIS_EVENTS_CAUSES_DEATH>();
		
        internal HisEventsCausesDeathCreate()
            : base()
        {

        }

        internal HisEventsCausesDeathCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EVENTS_CAUSES_DEATH data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEventsCausesDeathCheck checker = new HisEventsCausesDeathCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.EVENTS_CAUSES_DEATH_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisEventsCausesDeathDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEventsCausesDeath_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEventsCausesDeath that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisEventsCausesDeaths.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisEventsCausesDeaths))
            {
                if (!DAOWorker.HisEventsCausesDeathDAO.TruncateList(this.recentHisEventsCausesDeaths))
                {
                    LogSystem.Warn("Rollback du lieu HisEventsCausesDeath that bai, can kiem tra lai." + LogUtil.TraceData("recentHisEventsCausesDeaths", this.recentHisEventsCausesDeaths));
                }
				this.recentHisEventsCausesDeaths = null;
            }
        }
    }
}
