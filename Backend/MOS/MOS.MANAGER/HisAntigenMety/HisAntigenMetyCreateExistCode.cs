using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntigenMety
{
    partial class HisAntigenMetyCreate : BusinessBase
    {
		private List<HIS_ANTIGEN_METY> recentHisAntigenMetys = new List<HIS_ANTIGEN_METY>();
		
        internal HisAntigenMetyCreate()
            : base()
        {

        }

        internal HisAntigenMetyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ANTIGEN_METY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAntigenMetyCheck checker = new HisAntigenMetyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ANTIGEN_METY_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisAntigenMetyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAntigenMety_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAntigenMety that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAntigenMetys.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisAntigenMetys))
            {
                if (!DAOWorker.HisAntigenMetyDAO.TruncateList(this.recentHisAntigenMetys))
                {
                    LogSystem.Warn("Rollback du lieu HisAntigenMety that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAntigenMetys", this.recentHisAntigenMetys));
                }
				this.recentHisAntigenMetys = null;
            }
        }
    }
}
