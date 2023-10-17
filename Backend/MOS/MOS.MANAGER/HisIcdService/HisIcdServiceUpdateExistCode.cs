using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisIcdService
{
    partial class HisIcdServiceUpdate : BusinessBase
    {
		private List<HIS_ICD_SERVICE> beforeUpdateHisIcdServices = new List<HIS_ICD_SERVICE>();
		
        internal HisIcdServiceUpdate()
            : base()
        {

        }

        internal HisIcdServiceUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ICD_SERVICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisIcdServiceCheck checker = new HisIcdServiceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ICD_SERVICE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.ICD_SERVICE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisIcdServiceDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisIcdService_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisIcdService that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisIcdServices.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_ICD_SERVICE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisIcdServiceCheck checker = new HisIcdServiceCheck(param);
                List<HIS_ICD_SERVICE> listRaw = new List<HIS_ICD_SERVICE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ICD_SERVICE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisIcdServiceDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisIcdService_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisIcdService that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisIcdServices.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisIcdServices))
            {
                if (!DAOWorker.HisIcdServiceDAO.UpdateList(this.beforeUpdateHisIcdServices))
                {
                    LogSystem.Warn("Rollback du lieu HisIcdService that bai, can kiem tra lai." + LogUtil.TraceData("HisIcdServices", this.beforeUpdateHisIcdServices));
                }
				this.beforeUpdateHisIcdServices = null;
            }
        }
    }
}
