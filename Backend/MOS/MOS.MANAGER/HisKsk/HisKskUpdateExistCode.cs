using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKsk
{
    partial class HisKskUpdate : BusinessBase
    {
		private List<HIS_KSK> beforeUpdateHisKsks = new List<HIS_KSK>();
		
        internal HisKskUpdate()
            : base()
        {

        }

        internal HisKskUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_KSK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskCheck checker = new HisKskCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_KSK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.KSK_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisKskDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKsk_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKsk that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisKsks.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_KSK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskCheck checker = new HisKskCheck(param);
                List<HIS_KSK> listRaw = new List<HIS_KSK>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.KSK_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisKskDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKsk_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKsk that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisKsks.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisKsks))
            {
                if (!DAOWorker.HisKskDAO.UpdateList(this.beforeUpdateHisKsks))
                {
                    LogSystem.Warn("Rollback du lieu HisKsk that bai, can kiem tra lai." + LogUtil.TraceData("HisKsks", this.beforeUpdateHisKsks));
                }
				this.beforeUpdateHisKsks = null;
            }
        }
    }
}
