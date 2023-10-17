using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisIcdCm
{
    partial class HisIcdCmUpdate : BusinessBase
    {
		private List<HIS_ICD_CM> beforeUpdateHisIcdCms = new List<HIS_ICD_CM>();
		
        internal HisIcdCmUpdate()
            : base()
        {

        }

        internal HisIcdCmUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ICD_CM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisIcdCmCheck checker = new HisIcdCmCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ICD_CM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.ICD_CM_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisIcdCms.Add(raw);
					if (!DAOWorker.HisIcdCmDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisIcdCm_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisIcdCm that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_ICD_CM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisIcdCmCheck checker = new HisIcdCmCheck(param);
                List<HIS_ICD_CM> listRaw = new List<HIS_ICD_CM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ICD_CM_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisIcdCms.AddRange(listRaw);
					if (!DAOWorker.HisIcdCmDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisIcdCm_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisIcdCm that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisIcdCms))
            {
                if (!new HisIcdCmUpdate(param).UpdateList(this.beforeUpdateHisIcdCms))
                {
                    LogSystem.Warn("Rollback du lieu HisIcdCm that bai, can kiem tra lai." + LogUtil.TraceData("HisIcdCms", this.beforeUpdateHisIcdCms));
                }
            }
        }
    }
}
