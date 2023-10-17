using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisImpUserTempDt
{
    partial class HisImpUserTempDtUpdate : BusinessBase
    {
		private List<HIS_IMP_USER_TEMP_DT> beforeUpdateHisImpUserTempDts = new List<HIS_IMP_USER_TEMP_DT>();
		
        internal HisImpUserTempDtUpdate()
            : base()
        {

        }

        internal HisImpUserTempDtUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_IMP_USER_TEMP_DT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisImpUserTempDtCheck checker = new HisImpUserTempDtCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_IMP_USER_TEMP_DT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.IMP_USER_TEMP_DT_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisImpUserTempDtDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpUserTempDt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpUserTempDt that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisImpUserTempDts.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_IMP_USER_TEMP_DT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisImpUserTempDtCheck checker = new HisImpUserTempDtCheck(param);
                List<HIS_IMP_USER_TEMP_DT> listRaw = new List<HIS_IMP_USER_TEMP_DT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.IMP_USER_TEMP_DT_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisImpUserTempDtDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpUserTempDt_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpUserTempDt that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisImpUserTempDts.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisImpUserTempDts))
            {
                if (!DAOWorker.HisImpUserTempDtDAO.UpdateList(this.beforeUpdateHisImpUserTempDts))
                {
                    LogSystem.Warn("Rollback du lieu HisImpUserTempDt that bai, can kiem tra lai." + LogUtil.TraceData("HisImpUserTempDts", this.beforeUpdateHisImpUserTempDts));
                }
				this.beforeUpdateHisImpUserTempDts = null;
            }
        }
    }
}
