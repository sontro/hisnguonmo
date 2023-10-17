using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServDebt
{
    partial class HisSereServDebtUpdate : BusinessBase
    {
		private List<HIS_SERE_SERV_DEBT> beforeUpdateHisSereServDebts = new List<HIS_SERE_SERV_DEBT>();
		
        internal HisSereServDebtUpdate()
            : base()
        {

        }

        internal HisSereServDebtUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERE_SERV_DEBT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServDebtCheck checker = new HisSereServDebtCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERE_SERV_DEBT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.SERE_SERV_DEBT_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisSereServDebtDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServDebt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServDebt that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisSereServDebts.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_SERE_SERV_DEBT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServDebtCheck checker = new HisSereServDebtCheck(param);
                List<HIS_SERE_SERV_DEBT> listRaw = new List<HIS_SERE_SERV_DEBT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.SERE_SERV_DEBT_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisSereServDebtDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServDebt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServDebt that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisSereServDebts.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSereServDebts))
            {
                if (!DAOWorker.HisSereServDebtDAO.UpdateList(this.beforeUpdateHisSereServDebts))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServDebt that bai, can kiem tra lai." + LogUtil.TraceData("HisSereServDebts", this.beforeUpdateHisSereServDebts));
                }
				this.beforeUpdateHisSereServDebts = null;
            }
        }
    }
}
