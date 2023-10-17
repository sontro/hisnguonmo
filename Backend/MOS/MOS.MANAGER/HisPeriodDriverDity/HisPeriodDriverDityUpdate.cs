using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPeriodDriverDity
{
    partial class HisPeriodDriverDityUpdate : BusinessBase
    {
		private List<HIS_PERIOD_DRIVER_DITY> beforeUpdateHisPeriodDriverDitys = new List<HIS_PERIOD_DRIVER_DITY>();
		
        internal HisPeriodDriverDityUpdate()
            : base()
        {

        }

        internal HisPeriodDriverDityUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PERIOD_DRIVER_DITY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPeriodDriverDityCheck checker = new HisPeriodDriverDityCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PERIOD_DRIVER_DITY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisPeriodDriverDityDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPeriodDriverDity_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPeriodDriverDity that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisPeriodDriverDitys.Add(raw);
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

        internal bool UpdateList(List<HIS_PERIOD_DRIVER_DITY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPeriodDriverDityCheck checker = new HisPeriodDriverDityCheck(param);
                List<HIS_PERIOD_DRIVER_DITY> listRaw = new List<HIS_PERIOD_DRIVER_DITY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisPeriodDriverDityDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPeriodDriverDity_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPeriodDriverDity that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisPeriodDriverDitys.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPeriodDriverDitys))
            {
                if (!DAOWorker.HisPeriodDriverDityDAO.UpdateList(this.beforeUpdateHisPeriodDriverDitys))
                {
                    LogSystem.Warn("Rollback du lieu HisPeriodDriverDity that bai, can kiem tra lai." + LogUtil.TraceData("HisPeriodDriverDitys", this.beforeUpdateHisPeriodDriverDitys));
                }
				this.beforeUpdateHisPeriodDriverDitys = null;
            }
        }
    }
}
