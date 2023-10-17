using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBedBsty
{
    partial class HisBedBstyUpdate : BusinessBase
    {
		private List<HIS_BED_BSTY> beforeUpdateHisBedBstys = new List<HIS_BED_BSTY>();
		
        internal HisBedBstyUpdate()
            : base()
        {

        }

        internal HisBedBstyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BED_BSTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBedBstyCheck checker = new HisBedBstyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BED_BSTY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNotExisted(raw);
                if (valid)
                {
                    this.beforeUpdateHisBedBstys.Add(raw);
					if (!DAOWorker.HisBedBstyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBedBsty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBedBsty that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_BED_BSTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBedBstyCheck checker = new HisBedBstyCheck(param);
                List<HIS_BED_BSTY> listRaw = new List<HIS_BED_BSTY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisBedBstys.AddRange(listRaw);
					if (!DAOWorker.HisBedBstyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBedBsty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBedBsty that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBedBstys))
            {
                if (!new HisBedBstyUpdate(param).UpdateList(this.beforeUpdateHisBedBstys))
                {
                    LogSystem.Warn("Rollback du lieu HisBedBsty that bai, can kiem tra lai." + LogUtil.TraceData("HisBedBstys", this.beforeUpdateHisBedBstys));
                }
            }
        }
    }
}
