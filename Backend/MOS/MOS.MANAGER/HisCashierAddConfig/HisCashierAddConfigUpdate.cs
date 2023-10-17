using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCashierAddConfig
{
    partial class HisCashierAddConfigUpdate : BusinessBase
    {
		private List<HIS_CASHIER_ADD_CONFIG> beforeUpdateHisCashierAddConfigs = new List<HIS_CASHIER_ADD_CONFIG>();
		
        internal HisCashierAddConfigUpdate()
            : base()
        {

        }

        internal HisCashierAddConfigUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_CASHIER_ADD_CONFIG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCashierAddConfigCheck checker = new HisCashierAddConfigCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_CASHIER_ADD_CONFIG raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisCashierAddConfigDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCashierAddConfig_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCashierAddConfig that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisCashierAddConfigs.Add(raw);
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

        internal bool UpdateList(List<HIS_CASHIER_ADD_CONFIG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCashierAddConfigCheck checker = new HisCashierAddConfigCheck(param);
                List<HIS_CASHIER_ADD_CONFIG> listRaw = new List<HIS_CASHIER_ADD_CONFIG>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisCashierAddConfigDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCashierAddConfig_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCashierAddConfig that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisCashierAddConfigs.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisCashierAddConfigs))
            {
                if (!DAOWorker.HisCashierAddConfigDAO.UpdateList(this.beforeUpdateHisCashierAddConfigs))
                {
                    LogSystem.Warn("Rollback du lieu HisCashierAddConfig that bai, can kiem tra lai." + LogUtil.TraceData("HisCashierAddConfigs", this.beforeUpdateHisCashierAddConfigs));
                }
				this.beforeUpdateHisCashierAddConfigs = null;
            }
        }
    }
}
