using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDebateEkipUser
{
    partial class HisDebateEkipUserUpdate : BusinessBase
    {
		private List<HIS_DEBATE_EKIP_USER> beforeUpdateHisDebateEkipUsers = new List<HIS_DEBATE_EKIP_USER>();
		
        internal HisDebateEkipUserUpdate()
            : base()
        {

        }

        internal HisDebateEkipUserUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_DEBATE_EKIP_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebateEkipUserCheck checker = new HisDebateEkipUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DEBATE_EKIP_USER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisDebateEkipUserDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebateEkipUser_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDebateEkipUser that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisDebateEkipUsers.Add(raw);
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

        internal bool UpdateList(List<HIS_DEBATE_EKIP_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDebateEkipUserCheck checker = new HisDebateEkipUserCheck(param);
                List<HIS_DEBATE_EKIP_USER> listRaw = new List<HIS_DEBATE_EKIP_USER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisDebateEkipUserDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebateEkipUser_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDebateEkipUser that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisDebateEkipUsers.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisDebateEkipUsers))
            {
                if (!DAOWorker.HisDebateEkipUserDAO.UpdateList(this.beforeUpdateHisDebateEkipUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisDebateEkipUser that bai, can kiem tra lai." + LogUtil.TraceData("HisDebateEkipUsers", this.beforeUpdateHisDebateEkipUsers));
                }
				this.beforeUpdateHisDebateEkipUsers = null;
            }
        }
    }
}
