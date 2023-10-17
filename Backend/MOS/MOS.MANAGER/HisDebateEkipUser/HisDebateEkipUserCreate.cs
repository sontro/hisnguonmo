using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateEkipUser
{
    partial class HisDebateEkipUserCreate : BusinessBase
    {
		private List<HIS_DEBATE_EKIP_USER> recentHisDebateEkipUsers = new List<HIS_DEBATE_EKIP_USER>();
		
        internal HisDebateEkipUserCreate()
            : base()
        {

        }

        internal HisDebateEkipUserCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_DEBATE_EKIP_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebateEkipUserCheck checker = new HisDebateEkipUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisDebateEkipUserDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebateEkipUser_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDebateEkipUser that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisDebateEkipUsers.Add(data);
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
		
		internal bool CreateList(List<HIS_DEBATE_EKIP_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDebateEkipUserCheck checker = new HisDebateEkipUserCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisDebateEkipUserDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebateEkipUser_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisDebateEkipUser that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisDebateEkipUsers.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisDebateEkipUsers))
            {
                if (!DAOWorker.HisDebateEkipUserDAO.TruncateList(this.recentHisDebateEkipUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisDebateEkipUser that bai, can kiem tra lai." + LogUtil.TraceData("recentHisDebateEkipUsers", this.recentHisDebateEkipUsers));
                }
				this.recentHisDebateEkipUsers = null;
            }
        }
    }
}
