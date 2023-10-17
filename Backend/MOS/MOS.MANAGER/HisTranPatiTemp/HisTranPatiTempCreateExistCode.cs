using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTranPatiTemp
{
    partial class HisTranPatiTempCreate : BusinessBase
    {
		private List<HIS_TRAN_PATI_TEMP> recentHisTranPatiTemps = new List<HIS_TRAN_PATI_TEMP>();
		
        internal HisTranPatiTempCreate()
            : base()
        {

        }

        internal HisTranPatiTempCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_TRAN_PATI_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTranPatiTempCheck checker = new HisTranPatiTempCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.TRAN_PATI_TEMP_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisTranPatiTempDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTranPatiTemp_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTranPatiTemp that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisTranPatiTemps.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisTranPatiTemps))
            {
                if (!DAOWorker.HisTranPatiTempDAO.TruncateList(this.recentHisTranPatiTemps))
                {
                    LogSystem.Warn("Rollback du lieu HisTranPatiTemp that bai, can kiem tra lai." + LogUtil.TraceData("recentHisTranPatiTemps", this.recentHisTranPatiTemps));
                }
				this.recentHisTranPatiTemps = null;
            }
        }
    }
}
