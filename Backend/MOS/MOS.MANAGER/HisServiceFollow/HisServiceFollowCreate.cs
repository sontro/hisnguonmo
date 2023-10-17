using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceFollow
{
    partial class HisServiceFollowCreate : BusinessBase
    {
		private List<HIS_SERVICE_FOLLOW> recentHisServiceFollows = new List<HIS_SERVICE_FOLLOW>();
		
        internal HisServiceFollowCreate()
            : base()
        {

        }

        internal HisServiceFollowCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERVICE_FOLLOW data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceFollowCheck checker = new HisServiceFollowCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisServiceFollowDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceFollow_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisServiceFollow that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisServiceFollows.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisServiceFollows))
            {
                if (!new HisServiceFollowTruncate(param).TruncateList(this.recentHisServiceFollows))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceFollow that bai, can kiem tra lai." + LogUtil.TraceData("recentHisServiceFollows", this.recentHisServiceFollows));
                }
            }
        }
    }
}
