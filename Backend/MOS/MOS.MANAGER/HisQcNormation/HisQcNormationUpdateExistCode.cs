using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisQcNormation
{
    partial class HisQcNormationUpdate : BusinessBase
    {
		private List<HIS_QC_NORMATION> beforeUpdateHisQcNormations = new List<HIS_QC_NORMATION>();
		
        internal HisQcNormationUpdate()
            : base()
        {

        }

        internal HisQcNormationUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_QC_NORMATION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisQcNormationCheck checker = new HisQcNormationCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_QC_NORMATION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.QC_NORMATION_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisQcNormationDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisQcNormation_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisQcNormation that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisQcNormations.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_QC_NORMATION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisQcNormationCheck checker = new HisQcNormationCheck(param);
                List<HIS_QC_NORMATION> listRaw = new List<HIS_QC_NORMATION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.QC_NORMATION_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisQcNormationDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisQcNormation_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisQcNormation that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisQcNormations.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisQcNormations))
            {
                if (!DAOWorker.HisQcNormationDAO.UpdateList(this.beforeUpdateHisQcNormations))
                {
                    LogSystem.Warn("Rollback du lieu HisQcNormation that bai, can kiem tra lai." + LogUtil.TraceData("HisQcNormations", this.beforeUpdateHisQcNormations));
                }
				this.beforeUpdateHisQcNormations = null;
            }
        }
    }
}
