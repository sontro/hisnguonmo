using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServDebt
{
    partial class HisSereServDebtCreate : BusinessBase
    {
		private List<HIS_SERE_SERV_DEBT> recentHisSereServDebts = new List<HIS_SERE_SERV_DEBT>();
		
        internal HisSereServDebtCreate()
            : base()
        {

        }

        internal HisSereServDebtCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERE_SERV_DEBT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServDebtCheck checker = new HisSereServDebtCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.SERE_SERV_DEBT_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisSereServDebtDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServDebt_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServDebt that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSereServDebts.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisSereServDebts))
            {
                if (!DAOWorker.HisSereServDebtDAO.TruncateList(this.recentHisSereServDebts))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServDebt that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSereServDebts", this.recentHisSereServDebts));
                }
				this.recentHisSereServDebts = null;
            }
        }
    }
}
