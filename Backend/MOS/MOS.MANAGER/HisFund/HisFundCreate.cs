using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFund
{
    partial class HisFundCreate : BusinessBase
    {
		private List<HIS_FUND> recentHisFunds = new List<HIS_FUND>();
		
        internal HisFundCreate()
            : base()
        {

        }

        internal HisFundCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_FUND data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisFundCheck checker = new HisFundCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisFundDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFund_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisFund that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisFunds.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisFunds))
            {
                if (!new HisFundTruncate(param).TruncateList(this.recentHisFunds))
                {
                    LogSystem.Warn("Rollback du lieu HisFund that bai, can kiem tra lai." + LogUtil.TraceData("recentHisFunds", this.recentHisFunds));
                }
            }
        }
    }
}
