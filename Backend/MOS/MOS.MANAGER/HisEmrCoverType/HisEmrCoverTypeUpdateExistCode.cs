using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEmrCoverType
{
    partial class HisEmrCoverTypeUpdate : BusinessBase
    {
		private List<HIS_EMR_COVER_TYPE> beforeUpdateHisEmrCoverTypes = new List<HIS_EMR_COVER_TYPE>();
		
        internal HisEmrCoverTypeUpdate()
            : base()
        {

        }

        internal HisEmrCoverTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EMR_COVER_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmrCoverTypeCheck checker = new HisEmrCoverTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EMR_COVER_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.EMR_COVER_TYPE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisEmrCoverTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmrCoverType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEmrCoverType that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisEmrCoverTypes.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_EMR_COVER_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEmrCoverTypeCheck checker = new HisEmrCoverTypeCheck(param);
                List<HIS_EMR_COVER_TYPE> listRaw = new List<HIS_EMR_COVER_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.EMR_COVER_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisEmrCoverTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmrCoverType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEmrCoverType that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisEmrCoverTypes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisEmrCoverTypes))
            {
                if (!DAOWorker.HisEmrCoverTypeDAO.UpdateList(this.beforeUpdateHisEmrCoverTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisEmrCoverType that bai, can kiem tra lai." + LogUtil.TraceData("HisEmrCoverTypes", this.beforeUpdateHisEmrCoverTypes));
                }
				this.beforeUpdateHisEmrCoverTypes = null;
            }
        }
    }
}
