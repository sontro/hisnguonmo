using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskOther
{
    partial class HisKskOtherCreate : BusinessBase
    {
		private List<HIS_KSK_OTHER> recentHisKskOthers = new List<HIS_KSK_OTHER>();
		
        internal HisKskOtherCreate()
            : base()
        {

        }

        internal HisKskOtherCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_KSK_OTHER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskOtherCheck checker = new HisKskOtherCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.KSK_OTHER_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisKskOtherDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskOther_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKskOther that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisKskOthers.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisKskOthers))
            {
                if (!DAOWorker.HisKskOtherDAO.TruncateList(this.recentHisKskOthers))
                {
                    LogSystem.Warn("Rollback du lieu HisKskOther that bai, can kiem tra lai." + LogUtil.TraceData("recentHisKskOthers", this.recentHisKskOthers));
                }
				this.recentHisKskOthers = null;
            }
        }
    }
}
