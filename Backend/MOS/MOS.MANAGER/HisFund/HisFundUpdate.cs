using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisFund
{
    partial class HisFundUpdate : BusinessBase
    {
		private List<HIS_FUND> beforeUpdateHisFunds = new List<HIS_FUND>();
		
        internal HisFundUpdate()
            : base()
        {

        }

        internal HisFundUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_FUND data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisFundCheck checker = new HisFundCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_FUND raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisFunds.Add(raw);
					if (!DAOWorker.HisFundDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFund_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisFund that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_FUND> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisFundCheck checker = new HisFundCheck(param);
                List<HIS_FUND> listRaw = new List<HIS_FUND>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisFunds.AddRange(listRaw);
					if (!DAOWorker.HisFundDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisFund_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisFund that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisFunds))
            {
                if (!new HisFundUpdate(param).UpdateList(this.beforeUpdateHisFunds))
                {
                    LogSystem.Warn("Rollback du lieu HisFund that bai, can kiem tra lai." + LogUtil.TraceData("HisFunds", this.beforeUpdateHisFunds));
                }
            }
        }
    }
}
