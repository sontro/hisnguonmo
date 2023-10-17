using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBank
{
    partial class HisBankCreate : BusinessBase
    {
		private List<HIS_BANK> recentHisBanks = new List<HIS_BANK>();
		
        internal HisBankCreate()
            : base()
        {

        }

        internal HisBankCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_BANK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBankCheck checker = new HisBankCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.BANK_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisBankDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBank_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisBank that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisBanks.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisBanks))
            {
                if (!DAOWorker.HisBankDAO.TruncateList(this.recentHisBanks))
                {
                    LogSystem.Warn("Rollback du lieu HisBank that bai, can kiem tra lai." + LogUtil.TraceData("recentHisBanks", this.recentHisBanks));
                }
				this.recentHisBanks = null;
            }
        }
    }
}
