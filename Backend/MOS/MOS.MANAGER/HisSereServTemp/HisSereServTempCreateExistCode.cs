using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServTemp
{
    partial class HisSereServTempCreate : BusinessBase
    {
		private List<HIS_SERE_SERV_TEMP> recentHisSereServTemps = new List<HIS_SERE_SERV_TEMP>();
		
        internal HisSereServTempCreate()
            : base()
        {

        }

        internal HisSereServTempCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERE_SERV_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServTempCheck checker = new HisSereServTempCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.SERE_SERV_TEMP_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisSereServTempDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServTemp_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServTemp that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSereServTemps.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisSereServTemps))
            {
                if (!DAOWorker.HisSereServTempDAO.TruncateList(this.recentHisSereServTemps))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServTemp that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSereServTemps", this.recentHisSereServTemps));
                }
            }
        }
    }
}
