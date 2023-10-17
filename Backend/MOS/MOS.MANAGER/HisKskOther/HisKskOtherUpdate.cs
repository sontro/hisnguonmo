using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskOther
{
    partial class HisKskOtherUpdate : BusinessBase
    {
		private List<HIS_KSK_OTHER> beforeUpdateHisKskOthers = new List<HIS_KSK_OTHER>();
		
        internal HisKskOtherUpdate()
            : base()
        {

        }

        internal HisKskOtherUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_KSK_OTHER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskOtherCheck checker = new HisKskOtherCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_KSK_OTHER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisKskOtherDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskOther_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskOther that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisKskOthers.Add(raw);
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

        internal bool UpdateList(List<HIS_KSK_OTHER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskOtherCheck checker = new HisKskOtherCheck(param);
                List<HIS_KSK_OTHER> listRaw = new List<HIS_KSK_OTHER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisKskOtherDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskOther_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskOther that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisKskOthers.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisKskOthers))
            {
                if (!DAOWorker.HisKskOtherDAO.UpdateList(this.beforeUpdateHisKskOthers))
                {
                    LogSystem.Warn("Rollback du lieu HisKskOther that bai, can kiem tra lai." + LogUtil.TraceData("HisKskOthers", this.beforeUpdateHisKskOthers));
                }
				this.beforeUpdateHisKskOthers = null;
            }
        }
    }
}
