using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTransfusion
{
    partial class HisTransfusionUpdate : BusinessBase
    {
		private List<HIS_TRANSFUSION> beforeUpdateHisTransfusions = new List<HIS_TRANSFUSION>();
		
        internal HisTransfusionUpdate()
            : base()
        {

        }

        internal HisTransfusionUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TRANSFUSION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTransfusionCheck checker = new HisTransfusionCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_TRANSFUSION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.TRANSFUSION_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisTransfusionDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransfusion_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTransfusion that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisTransfusions.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_TRANSFUSION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTransfusionCheck checker = new HisTransfusionCheck(param);
                List<HIS_TRANSFUSION> listRaw = new List<HIS_TRANSFUSION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.TRANSFUSION_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisTransfusionDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransfusion_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTransfusion that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisTransfusions.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisTransfusions))
            {
                if (!DAOWorker.HisTransfusionDAO.UpdateList(this.beforeUpdateHisTransfusions))
                {
                    LogSystem.Warn("Rollback du lieu HisTransfusion that bai, can kiem tra lai." + LogUtil.TraceData("HisTransfusions", this.beforeUpdateHisTransfusions));
                }
				this.beforeUpdateHisTransfusions = null;
            }
        }
    }
}
