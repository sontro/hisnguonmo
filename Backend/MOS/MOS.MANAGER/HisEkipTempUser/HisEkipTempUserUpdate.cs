using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEkipTempUser
{
    partial class HisEkipTempUserUpdate : BusinessBase
    {
		private List<HIS_EKIP_TEMP_USER> beforeUpdateHisEkipTempUsers = new List<HIS_EKIP_TEMP_USER>();
		
        internal HisEkipTempUserUpdate()
            : base()
        {

        }

        internal HisEkipTempUserUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EKIP_TEMP_USER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEkipTempUserCheck checker = new HisEkipTempUserCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EKIP_TEMP_USER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisEkipTempUsers.Add(raw);
					if (!DAOWorker.HisEkipTempUserDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkipTempUser_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEkipTempUser that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_EKIP_TEMP_USER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEkipTempUserCheck checker = new HisEkipTempUserCheck(param);
                List<HIS_EKIP_TEMP_USER> listRaw = new List<HIS_EKIP_TEMP_USER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisEkipTempUsers.AddRange(listRaw);
					if (!DAOWorker.HisEkipTempUserDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEkipTempUser_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEkipTempUser that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisEkipTempUsers))
            {
                if (!DAOWorker.HisEkipTempUserDAO.UpdateList(this.beforeUpdateHisEkipTempUsers))
                {
                    LogSystem.Warn("Rollback du lieu HisEkipTempUser that bai, can kiem tra lai." + LogUtil.TraceData("HisEkipTempUsers", this.beforeUpdateHisEkipTempUsers));
                }
                this.beforeUpdateHisEkipTempUsers = null;
            }
        }
    }
}
