using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDesk
{
    partial class HisDeskCreate : BusinessBase
    {
		private List<HIS_DESK> recentHisDesks = new List<HIS_DESK>();
		
        internal HisDeskCreate()
            : base()
        {

        }

        internal HisDeskCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DESK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDeskCheck checker = new HisDeskCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.DESK_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisDeskDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDesk_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDesk that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisDesks.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisDesks))
            {
                if (!DAOWorker.HisDeskDAO.TruncateList(this.recentHisDesks))
                {
                    LogSystem.Warn("Rollback du lieu HisDesk that bai, can kiem tra lai." + LogUtil.TraceData("recentHisDesks", this.recentHisDesks));
                }
				this.recentHisDesks = null;
            }
        }
    }
}
