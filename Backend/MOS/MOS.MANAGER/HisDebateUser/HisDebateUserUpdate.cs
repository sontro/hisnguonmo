using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDebateUser
{
    partial class HisDebateUserUpdate : BusinessBase
    {
        private List<HIS_DEBATE_USER> beforeUpdateHisDebateUsers = new List<HIS_DEBATE_USER>();

        internal HisDebateUserUpdate()
            : base()
        {
        }

        internal HisDebateUserUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {
        }

        internal bool Update(HIS_DEBATE_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebateUserCheck checker = new HisDebateUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DEBATE_USER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisDebateUsers.Add(raw);
                    if (!DAOWorker.HisDebateUserDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebateUser_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDebateUser that bai." + LogUtil.TraceData("data", data));
                    }

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

        private bool UpdateList(List<HIS_DEBATE_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDebateUserCheck checker = new HisDebateUserCheck(param);
                List<HIS_DEBATE_USER> listRaw = new List<HIS_DEBATE_USER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisDebateUsers.AddRange(listRaw);
                    if (!DAOWorker.HisDebateUserDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebateUser_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDebateUser that bai." + LogUtil.TraceData("listData", listData));
                    }
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisDebateUsers))
            {
                if (!this.UpdateList(this.beforeUpdateHisDebateUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisDebateUser that bai, can kiem tra lai." + LogUtil.TraceData("HisDebateUsers", this.beforeUpdateHisDebateUsers));
                }
            }
        }
    }
}
