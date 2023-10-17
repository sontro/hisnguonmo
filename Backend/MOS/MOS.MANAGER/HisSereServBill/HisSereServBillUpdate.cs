using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServBill
{
    partial class HisSereServBillUpdate : BusinessBase
    {
		private List<HIS_SERE_SERV_BILL> beforeUpdateHisSereServBills = new List<HIS_SERE_SERV_BILL>();
		
        internal HisSereServBillUpdate()
            : base()
        {

        }

        internal HisSereServBillUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERE_SERV_BILL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServBillCheck checker = new HisSereServBillCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERE_SERV_BILL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
					if (!DAOWorker.HisSereServBillDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServBill_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServBill that bai." + LogUtil.TraceData("data", data));
                    }

                    this.beforeUpdateHisSereServBills.Add(raw);   
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

        internal bool UpdateList(List<HIS_SERE_SERV_BILL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServBillCheck checker = new HisSereServBillCheck(param);
                List<HIS_SERE_SERV_BILL> listRaw = new List<HIS_SERE_SERV_BILL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisSereServBillDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServBill_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServBill that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisSereServBills.AddRange(listRaw);
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

        internal bool UpdateList(List<HIS_SERE_SERV_BILL> listData, List<HIS_SERE_SERV_BILL> beforeUpdates)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServBillCheck checker = new HisSereServBillCheck(param);
                valid = valid && checker.IsUnLock(beforeUpdates);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSereServBillDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServBill_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServBill that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisSereServBills.AddRange(beforeUpdates);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSereServBills))
            {
                if (!new HisSereServBillUpdate(param).UpdateList(this.beforeUpdateHisSereServBills))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServBill that bai, can kiem tra lai." + LogUtil.TraceData("HisSereServBills", this.beforeUpdateHisSereServBills));
                }
            }
        }
    }
}
