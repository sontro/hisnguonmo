using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCashout
{
    partial class HisCashoutUpdate : BusinessBase
    {
		private List<HIS_CASHOUT> beforeUpdateHisCashouts = new List<HIS_CASHOUT>();
		
        internal HisCashoutUpdate()
            : base()
        {

        }

        internal HisCashoutUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_CASHOUT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCashoutCheck checker = new HisCashoutCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_CASHOUT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.CASHOUT_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisCashouts.Add(raw);
					if (!DAOWorker.HisCashoutDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCashout_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCashout that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_CASHOUT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCashoutCheck checker = new HisCashoutCheck(param);
                List<HIS_CASHOUT> listRaw = new List<HIS_CASHOUT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.CASHOUT_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisCashouts.AddRange(listRaw);
					if (!DAOWorker.HisCashoutDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCashout_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCashout that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisCashouts))
            {
                if (!new HisCashoutUpdate(param).UpdateList(this.beforeUpdateHisCashouts))
                {
                    LogSystem.Warn("Rollback du lieu HisCashout that bai, can kiem tra lai." + LogUtil.TraceData("HisCashouts", this.beforeUpdateHisCashouts));
                }
            }
        }
    }
}
