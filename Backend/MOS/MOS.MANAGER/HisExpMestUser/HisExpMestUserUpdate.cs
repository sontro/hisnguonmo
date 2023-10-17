using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisExpMestUser
{
    partial class HisExpMestUserUpdate : BusinessBase
    {
		private List<HIS_EXP_MEST_USER> beforeUpdateHisExpMestUsers = new List<HIS_EXP_MEST_USER>();
		
        internal HisExpMestUserUpdate()
            : base()
        {

        }

        internal HisExpMestUserUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EXP_MEST_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisExpMestUserCheck checker = new HisExpMestUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EXP_MEST_USER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisExpMestUsers.Add(raw);
					if (!DAOWorker.HisExpMestUserDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestUser_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMestUser that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_EXP_MEST_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisExpMestUserCheck checker = new HisExpMestUserCheck(param);
                List<HIS_EXP_MEST_USER> listRaw = new List<HIS_EXP_MEST_USER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisExpMestUsers.AddRange(listRaw);
					if (!DAOWorker.HisExpMestUserDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisExpMestUser_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisExpMestUser that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisExpMestUsers))
            {
                if (!new HisExpMestUserUpdate(param).UpdateList(this.beforeUpdateHisExpMestUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisExpMestUser that bai, can kiem tra lai." + LogUtil.TraceData("HisExpMestUsers", this.beforeUpdateHisExpMestUsers));
                }
            }
        }
    }
}
