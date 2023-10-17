using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKsk
{
    partial class HisKskCreate : BusinessBase
    {
		private List<HIS_KSK> recentHisKsks = new List<HIS_KSK>();
		
        internal HisKskCreate()
            : base()
        {

        }

        internal HisKskCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_KSK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskCheck checker = new HisKskCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.KSK_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisKskDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKsk_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisKsk that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisKsks.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisKsks))
            {
                if (!DAOWorker.HisKskDAO.TruncateList(this.recentHisKsks))
                {
                    LogSystem.Warn("Rollback du lieu HisKsk that bai, can kiem tra lai." + LogUtil.TraceData("recentHisKsks", this.recentHisKsks));
                }
				this.recentHisKsks = null;
            }
        }
    }
}
