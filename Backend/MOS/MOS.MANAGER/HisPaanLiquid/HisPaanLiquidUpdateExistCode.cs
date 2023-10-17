using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPaanLiquid
{
    partial class HisPaanLiquidUpdate : BusinessBase
    {
		private List<HIS_PAAN_LIQUID> beforeUpdateHisPaanLiquids = new List<HIS_PAAN_LIQUID>();
		
        internal HisPaanLiquidUpdate()
            : base()
        {

        }

        internal HisPaanLiquidUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PAAN_LIQUID data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPaanLiquidCheck checker = new HisPaanLiquidCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PAAN_LIQUID raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.PAAN_LIQUID_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisPaanLiquids.Add(raw);
					if (!DAOWorker.HisPaanLiquidDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPaanLiquid_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPaanLiquid that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_PAAN_LIQUID> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPaanLiquidCheck checker = new HisPaanLiquidCheck(param);
                List<HIS_PAAN_LIQUID> listRaw = new List<HIS_PAAN_LIQUID>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.PAAN_LIQUID_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisPaanLiquids.AddRange(listRaw);
					if (!DAOWorker.HisPaanLiquidDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPaanLiquid_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPaanLiquid that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisPaanLiquids))
            {
                if (!new HisPaanLiquidUpdate(param).UpdateList(this.beforeUpdateHisPaanLiquids))
                {
                    LogSystem.Warn("Rollback du lieu HisPaanLiquid that bai, can kiem tra lai." + LogUtil.TraceData("HisPaanLiquids", this.beforeUpdateHisPaanLiquids));
                }
            }
        }
    }
}
