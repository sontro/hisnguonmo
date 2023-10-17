using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMatyMaty
{
    partial class HisMatyMatyCreate : BusinessBase
    {
		private List<HIS_MATY_MATY> recentHisMatyMatys = new List<HIS_MATY_MATY>();
		
        internal HisMatyMatyCreate()
            : base()
        {

        }

        internal HisMatyMatyCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MATY_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMatyMatyCheck checker = new HisMatyMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.MATY_MATY_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisMatyMatyDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMatyMaty_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMatyMaty that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMatyMatys.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisMatyMatys))
            {
                if (!DAOWorker.HisMatyMatyDAO.TruncateList(this.recentHisMatyMatys))
                {
                    LogSystem.Warn("Rollback du lieu HisMatyMaty that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMatyMatys", this.recentHisMatyMatys));
                }
				this.recentHisMatyMatys = null;
            }
        }
    }
}
