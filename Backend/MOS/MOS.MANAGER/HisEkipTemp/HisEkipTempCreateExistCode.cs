using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipTemp
{
    partial class HisEkipTempCreate : BusinessBase
    {
		private List<HIS_EKIP_TEMP> recentHisEkipTemps = new List<HIS_EKIP_TEMP>();
		
        internal HisEkipTempCreate()
            : base()
        {

        }

        internal HisEkipTempCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_EKIP_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEkipTempCheck checker = new HisEkipTempCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.EKIP_TEMP_CODE, null);
                valid = valid && checker.IsNotDuplicate(data);
                if (valid)
                {
					if (!DAOWorker.HisEkipTempDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkipTemp_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisEkipTemp that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisEkipTemps.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisEkipTemps))
            {
                if (!DAOWorker.HisEkipTempDAO.TruncateList(this.recentHisEkipTemps))
                {
                    LogSystem.Warn("Rollback du lieu HisEkipTemp that bai, can kiem tra lai." + LogUtil.TraceData("recentHisEkipTemps", this.recentHisEkipTemps));
                }
                this.recentHisEkipTemps = null;
            }
        }
    }
}
