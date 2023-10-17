using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisContact
{
    partial class HisContactUpdate : BusinessBase
    {
		private List<HIS_CONTACT> beforeUpdateHisContacts = new List<HIS_CONTACT>();
		
        internal HisContactUpdate()
            : base()
        {

        }

        internal HisContactUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_CONTACT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisContactCheck checker = new HisContactCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_CONTACT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisContactDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisContact_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisContact that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisContacts.Add(raw);
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

        internal bool UpdateList(List<HIS_CONTACT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisContactCheck checker = new HisContactCheck(param);
                List<HIS_CONTACT> listRaw = new List<HIS_CONTACT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisContactDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisContact_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisContact that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisContacts.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisContacts))
            {
                if (!DAOWorker.HisContactDAO.UpdateList(this.beforeUpdateHisContacts))
                {
                    LogSystem.Warn("Rollback du lieu HisContact that bai, can kiem tra lai." + LogUtil.TraceData("HisContacts", this.beforeUpdateHisContacts));
                }
				this.beforeUpdateHisContacts = null;
            }
        }
    }
}
