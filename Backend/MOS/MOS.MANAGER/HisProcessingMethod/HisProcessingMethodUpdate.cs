using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisProcessingMethod
{
    partial class HisProcessingMethodUpdate : BusinessBase
    {
		private List<HIS_PROCESSING_METHOD> beforeUpdateHisProcessingMethods = new List<HIS_PROCESSING_METHOD>();
		
        internal HisProcessingMethodUpdate()
            : base()
        {

        }

        internal HisProcessingMethodUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PROCESSING_METHOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisProcessingMethodCheck checker = new HisProcessingMethodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PROCESSING_METHOD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.PROCESSING_METHOD_CODE, data.ID);
                if (valid)
                {                    
					if (!DAOWorker.HisProcessingMethodDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisProcessingMethod_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisProcessingMethod that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisProcessingMethods.Add(raw);
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

        internal bool UpdateList(List<HIS_PROCESSING_METHOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisProcessingMethodCheck checker = new HisProcessingMethodCheck(param);
                List<HIS_PROCESSING_METHOD> listRaw = new List<HIS_PROCESSING_METHOD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.PROCESSING_METHOD_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisProcessingMethodDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisProcessingMethod_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisProcessingMethod that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisProcessingMethods.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisProcessingMethods))
            {
                if (!DAOWorker.HisProcessingMethodDAO.UpdateList(this.beforeUpdateHisProcessingMethods))
                {
                    LogSystem.Warn("Rollback du lieu HisProcessingMethod that bai, can kiem tra lai." + LogUtil.TraceData("HisProcessingMethods", this.beforeUpdateHisProcessingMethods));
                }
				this.beforeUpdateHisProcessingMethods = null;
            }
        }
    }
}
