using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCashout
{
    partial class HisCashoutCreate : BusinessBase
    {
		private List<HIS_CASHOUT> recentHisCashouts = new List<HIS_CASHOUT>();
		
        internal HisCashoutCreate()
            : base()
        {

        }

        internal HisCashoutCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_CASHOUT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCashoutCheck checker = new HisCashoutCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.CASHOUT_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisCashoutDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCashout_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCashout that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisCashouts.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisCashouts))
            {
                if (!new HisCashoutTruncate(param).TruncateList(this.recentHisCashouts))
                {
                    LogSystem.Warn("Rollback du lieu HisCashout that bai, can kiem tra lai." + LogUtil.TraceData("recentHisCashouts", this.recentHisCashouts));
                }
            }
        }
    }
}
