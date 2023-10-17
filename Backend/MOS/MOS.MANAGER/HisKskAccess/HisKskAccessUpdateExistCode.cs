using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskAccess
{
    partial class HisKskAccessUpdate : BusinessBase
    {
		private List<HIS_KSK_ACCESS> beforeUpdateHisKskAccesss = new List<HIS_KSK_ACCESS>();
		
        internal HisKskAccessUpdate()
            : base()
        {

        }

        internal HisKskAccessUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_KSK_ACCESS data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskAccessCheck checker = new HisKskAccessCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_KSK_ACCESS raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.KSK_ACCESS_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisKskAccessDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskAccess_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskAccess that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisKskAccesss.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_KSK_ACCESS> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskAccessCheck checker = new HisKskAccessCheck(param);
                List<HIS_KSK_ACCESS> listRaw = new List<HIS_KSK_ACCESS>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.KSK_ACCESS_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisKskAccessDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskAccess_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskAccess that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisKskAccesss.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisKskAccesss))
            {
                if (!DAOWorker.HisKskAccessDAO.UpdateList(this.beforeUpdateHisKskAccesss))
                {
                    LogSystem.Warn("Rollback du lieu HisKskAccess that bai, can kiem tra lai." + LogUtil.TraceData("HisKskAccesss", this.beforeUpdateHisKskAccesss));
                }
				this.beforeUpdateHisKskAccesss = null;
            }
        }
    }
}
