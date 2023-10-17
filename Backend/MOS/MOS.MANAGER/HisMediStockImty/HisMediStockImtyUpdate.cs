using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMediStockImty
{
    partial class HisMediStockImtyUpdate : BusinessBase
    {
		private List<HIS_MEDI_STOCK_IMTY> beforeUpdateHisMediStockImtys = new List<HIS_MEDI_STOCK_IMTY>();
		
        internal HisMediStockImtyUpdate()
            : base()
        {

        }

        internal HisMediStockImtyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDI_STOCK_IMTY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMediStockImtyCheck checker = new HisMediStockImtyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEDI_STOCK_IMTY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisMediStockImtys.Add(raw);
					if (!DAOWorker.HisMediStockImtyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediStockImty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediStockImty that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_MEDI_STOCK_IMTY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMediStockImtyCheck checker = new HisMediStockImtyCheck(param);
                List<HIS_MEDI_STOCK_IMTY> listRaw = new List<HIS_MEDI_STOCK_IMTY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisMediStockImtys.AddRange(listRaw);
					if (!DAOWorker.HisMediStockImtyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediStockImty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediStockImty that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMediStockImtys))
            {
                if (!DAOWorker.HisMediStockImtyDAO.UpdateList(this.beforeUpdateHisMediStockImtys))
                {
                    LogSystem.Warn("Rollback du lieu HisMediStockImty that bai, can kiem tra lai." + LogUtil.TraceData("HisMediStockImtys", this.beforeUpdateHisMediStockImtys));
                }
            }
        }
    }
}
