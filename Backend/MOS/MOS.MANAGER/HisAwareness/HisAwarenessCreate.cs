using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAwareness
{
    partial class HisAwarenessCreate : BusinessBase
    {
		private HIS_AWARENESS recentHisAwarenessDTO;
		
        internal HisAwarenessCreate()
            : base()
        {

        }

        internal HisAwarenessCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_AWARENESS data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAwarenessCheck checker = new HisAwarenessCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.AWARENESS_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisAwarenessDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAwareness_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAwareness that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAwarenessDTO = data;
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
            if (this.recentHisAwarenessDTO != null)
            {
                if (!new HisAwarenessTruncate(param).Truncate(this.recentHisAwarenessDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisAwareness that bai, can kiem tra lai." + LogUtil.TraceData("HisAwareness", this.recentHisAwarenessDTO));
                }
            }
        }
    }
}
