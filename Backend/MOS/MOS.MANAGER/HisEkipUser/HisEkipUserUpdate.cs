using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEkipUser
{
    partial class HisEkipUserUpdate : BusinessBase
    {
		private List<HIS_EKIP_USER> beforeUpdateHisEkipUsers = new List<HIS_EKIP_USER>();
		
        internal HisEkipUserUpdate()
            : base()
        {

        }

        internal HisEkipUserUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EKIP_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEkipUserCheck checker = new HisEkipUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EKIP_USER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisEkipUsers.Add(raw);
					if (!DAOWorker.HisEkipUserDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkipUser_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEkipUser that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_EKIP_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEkipUserCheck checker = new HisEkipUserCheck(param);
                List<HIS_EKIP_USER> listRaw = new List<HIS_EKIP_USER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisEkipUsers.AddRange(listRaw);
					if (!DAOWorker.HisEkipUserDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkipUser_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEkipUser that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisEkipUsers))
            {
                if (!new HisEkipUserUpdate(param).UpdateList(this.beforeUpdateHisEkipUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisEkipUser that bai, can kiem tra lai." + LogUtil.TraceData("HisEkipUsers", this.beforeUpdateHisEkipUsers));
                }
            }
        }
    }
}
