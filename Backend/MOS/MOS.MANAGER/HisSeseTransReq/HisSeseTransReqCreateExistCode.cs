using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSeseTransReq
{
    partial class HisSeseTransReqCreate : BusinessBase
    {
		private List<HIS_SESE_TRANS_REQ> recentHisSeseTransReqs = new List<HIS_SESE_TRANS_REQ>();
		
        internal HisSeseTransReqCreate()
            : base()
        {

        }

        internal HisSeseTransReqCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SESE_TRANS_REQ data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSeseTransReqCheck checker = new HisSeseTransReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.SESE_TRANS_REQ_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisSeseTransReqDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSeseTransReq_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSeseTransReq that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSeseTransReqs.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisSeseTransReqs))
            {
                if (!DAOWorker.HisSeseTransReqDAO.TruncateList(this.recentHisSeseTransReqs))
                {
                    LogSystem.Warn("Rollback du lieu HisSeseTransReq that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSeseTransReqs", this.recentHisSeseTransReqs));
                }
				this.recentHisSeseTransReqs = null;
            }
        }
    }
}
