using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccHealthStt
{
    partial class HisVaccHealthSttCreate : BusinessBase
    {
		private List<HIS_VACC_HEALTH_STT> recentHisVaccHealthStts = new List<HIS_VACC_HEALTH_STT>();
		
        internal HisVaccHealthSttCreate()
            : base()
        {

        }

        internal HisVaccHealthSttCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_VACC_HEALTH_STT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccHealthSttCheck checker = new HisVaccHealthSttCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.VACC_HEALTH_STT_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisVaccHealthSttDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccHealthStt_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccHealthStt that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisVaccHealthStts.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisVaccHealthStts))
            {
                if (!DAOWorker.HisVaccHealthSttDAO.TruncateList(this.recentHisVaccHealthStts))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccHealthStt that bai, can kiem tra lai." + LogUtil.TraceData("recentHisVaccHealthStts", this.recentHisVaccHealthStts));
                }
				this.recentHisVaccHealthStts = null;
            }
        }
    }
}
