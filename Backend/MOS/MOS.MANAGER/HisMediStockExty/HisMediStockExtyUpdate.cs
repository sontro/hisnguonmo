using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediStockExty
{
    partial class HisMediStockExtyUpdate : BusinessBase
    {
		private List<HIS_MEDI_STOCK_EXTY> beforeUpdateHisMediStockExtys = new List<HIS_MEDI_STOCK_EXTY>();
		
        internal HisMediStockExtyUpdate()
            : base()
        {

        }

        internal HisMediStockExtyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDI_STOCK_EXTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediStockExtyCheck checker = new HisMediStockExtyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEDI_STOCK_EXTY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisMediStockExtys.Add(raw);
					if (!DAOWorker.HisMediStockExtyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediStockExty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediStockExty that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_MEDI_STOCK_EXTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediStockExtyCheck checker = new HisMediStockExtyCheck(param);
                List<HIS_MEDI_STOCK_EXTY> listRaw = new List<HIS_MEDI_STOCK_EXTY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisMediStockExtys.AddRange(listRaw);
					if (!DAOWorker.HisMediStockExtyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediStockExty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediStockExty that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMediStockExtys))
            {
                if (!DAOWorker.HisMediStockExtyDAO.UpdateList(this.beforeUpdateHisMediStockExtys))
                {
                    LogSystem.Warn("Rollback du lieu HisMediStockExty that bai, can kiem tra lai." + LogUtil.TraceData("HisMediStockExtys", this.beforeUpdateHisMediStockExtys));
                }
            }
        }
    }
}
