using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisQcType
{
    partial class HisQcTypeUpdate : BusinessBase
    {
		private List<HIS_QC_TYPE> beforeUpdateHisQcTypes = new List<HIS_QC_TYPE>();
		
        internal HisQcTypeUpdate()
            : base()
        {

        }

        internal HisQcTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_QC_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisQcTypeCheck checker = new HisQcTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_QC_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.QC_TYPE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisQcTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisQcType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisQcType that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisQcTypes.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_QC_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisQcTypeCheck checker = new HisQcTypeCheck(param);
                List<HIS_QC_TYPE> listRaw = new List<HIS_QC_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.QC_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisQcTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisQcType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisQcType that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisQcTypes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisQcTypes))
            {
                if (!DAOWorker.HisQcTypeDAO.UpdateList(this.beforeUpdateHisQcTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisQcType that bai, can kiem tra lai." + LogUtil.TraceData("HisQcTypes", this.beforeUpdateHisQcTypes));
                }
				this.beforeUpdateHisQcTypes = null;
            }
        }
    }
}
