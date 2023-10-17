using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAtc
{
    partial class HisAtcCreate : BusinessBase
    {
		private List<HIS_ATC> recentHisAtcs = new List<HIS_ATC>();
		
        internal HisAtcCreate()
            : base()
        {

        }

        internal HisAtcCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ATC data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAtcCheck checker = new HisAtcCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ATC_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisAtcDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAtc_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAtc that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAtcs.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisAtcs))
            {
                if (!DAOWorker.HisAtcDAO.TruncateList(this.recentHisAtcs))
                {
                    LogSystem.Warn("Rollback du lieu HisAtc that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAtcs", this.recentHisAtcs));
                }
				this.recentHisAtcs = null;
            }
        }
    }
}
