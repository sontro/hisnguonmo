using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServBill
{
    partial class HisSereServBillCreate : BusinessBase
    {
		private List<HIS_SERE_SERV_BILL> recentHisSereServBills = new List<HIS_SERE_SERV_BILL>();
		
        internal HisSereServBillCreate()
            : base()
        {

        }

        internal HisSereServBillCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_SERE_SERV_BILL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServBillCheck checker = new HisSereServBillCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisSereServBillDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServBill_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServBill that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSereServBills.Add(data);
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
		
		internal bool CreateList(List<HIS_SERE_SERV_BILL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServBillCheck checker = new HisSereServBillCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSereServBillDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServBill_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServBill that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisSereServBills.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisSereServBills))
            {
                if (!DAOWorker.HisSereServBillDAO.TruncateList(this.recentHisSereServBills))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServBill that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSereServBills", this.recentHisSereServBills));
                }
            }
        }
    }
}
