using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisImpMestUser
{
    partial class HisImpMestUserUpdate : BusinessBase
    {
		private List<HIS_IMP_MEST_USER> beforeUpdateHisImpMestUsers = new List<HIS_IMP_MEST_USER>();
		
        internal HisImpMestUserUpdate()
            : base()
        {

        }

        internal HisImpMestUserUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_IMP_MEST_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpMestUserCheck checker = new HisImpMestUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_IMP_MEST_USER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisImpMestUsers.Add(raw);
					if (!DAOWorker.HisImpMestUserDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestUser_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMestUser that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_IMP_MEST_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpMestUserCheck checker = new HisImpMestUserCheck(param);
                List<HIS_IMP_MEST_USER> listRaw = new List<HIS_IMP_MEST_USER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisImpMestUsers.AddRange(listRaw);
					if (!DAOWorker.HisImpMestUserDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMestUser_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMestUser that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisImpMestUsers))
            {
                if (!new HisImpMestUserUpdate(param).UpdateList(this.beforeUpdateHisImpMestUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisImpMestUser that bai, can kiem tra lai." + LogUtil.TraceData("HisImpMestUsers", this.beforeUpdateHisImpMestUsers));
                }
            }
        }
    }
}
