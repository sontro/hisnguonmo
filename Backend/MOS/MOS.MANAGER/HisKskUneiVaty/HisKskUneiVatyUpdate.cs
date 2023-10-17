using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisKskUneiVaty
{
    partial class HisKskUneiVatyUpdate : BusinessBase
    {
		private List<HIS_KSK_UNEI_VATY> beforeUpdateHisKskUneiVatys = new List<HIS_KSK_UNEI_VATY>();
		
        internal HisKskUneiVatyUpdate()
            : base()
        {

        }

        internal HisKskUneiVatyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_KSK_UNEI_VATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisKskUneiVatyCheck checker = new HisKskUneiVatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_KSK_UNEI_VATY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisKskUneiVatyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskUneiVaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskUneiVaty that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisKskUneiVatys.Add(raw);
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

        internal bool UpdateList(List<HIS_KSK_UNEI_VATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisKskUneiVatyCheck checker = new HisKskUneiVatyCheck(param);
                List<HIS_KSK_UNEI_VATY> listRaw = new List<HIS_KSK_UNEI_VATY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisKskUneiVatyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisKskUneiVaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisKskUneiVaty that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisKskUneiVatys.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisKskUneiVatys))
            {
                if (!DAOWorker.HisKskUneiVatyDAO.UpdateList(this.beforeUpdateHisKskUneiVatys))
                {
                    LogSystem.Warn("Rollback du lieu HisKskUneiVaty that bai, can kiem tra lai." + LogUtil.TraceData("HisKskUneiVatys", this.beforeUpdateHisKskUneiVatys));
                }
				this.beforeUpdateHisKskUneiVatys = null;
            }
        }
    }
}
