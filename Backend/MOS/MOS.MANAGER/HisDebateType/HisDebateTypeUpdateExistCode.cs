using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDebateType
{
    partial class HisDebateTypeUpdate : BusinessBase
    {
		private List<HIS_DEBATE_TYPE> beforeUpdateHisDebateTypes = new List<HIS_DEBATE_TYPE>();
		
        internal HisDebateTypeUpdate()
            : base()
        {

        }

        internal HisDebateTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_DEBATE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebateTypeCheck checker = new HisDebateTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DEBATE_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.DEBATE_TYPE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisDebateTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebateType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDebateType that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisDebateTypes.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_DEBATE_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDebateTypeCheck checker = new HisDebateTypeCheck(param);
                List<HIS_DEBATE_TYPE> listRaw = new List<HIS_DEBATE_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.DEBATE_TYPE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisDebateTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebateType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDebateType that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisDebateTypes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisDebateTypes))
            {
                if (!DAOWorker.HisDebateTypeDAO.UpdateList(this.beforeUpdateHisDebateTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisDebateType that bai, can kiem tra lai." + LogUtil.TraceData("HisDebateTypes", this.beforeUpdateHisDebateTypes));
                }
				this.beforeUpdateHisDebateTypes = null;
            }
        }
    }
}
