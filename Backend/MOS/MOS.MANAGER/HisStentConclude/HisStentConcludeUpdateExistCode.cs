using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisStentConclude
{
    partial class HisStentConcludeUpdate : BusinessBase
    {
		private List<HIS_STENT_CONCLUDE> beforeUpdateHisStentConcludes = new List<HIS_STENT_CONCLUDE>();
		
        internal HisStentConcludeUpdate()
            : base()
        {

        }

        internal HisStentConcludeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_STENT_CONCLUDE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisStentConcludeCheck checker = new HisStentConcludeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_STENT_CONCLUDE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.STENT_CONCLUDE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisStentConcludeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisStentConclude_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisStentConclude that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisStentConcludes.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_STENT_CONCLUDE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisStentConcludeCheck checker = new HisStentConcludeCheck(param);
                List<HIS_STENT_CONCLUDE> listRaw = new List<HIS_STENT_CONCLUDE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.STENT_CONCLUDE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisStentConcludeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisStentConclude_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisStentConclude that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisStentConcludes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisStentConcludes))
            {
                if (!DAOWorker.HisStentConcludeDAO.UpdateList(this.beforeUpdateHisStentConcludes))
                {
                    LogSystem.Warn("Rollback du lieu HisStentConclude that bai, can kiem tra lai." + LogUtil.TraceData("HisStentConcludes", this.beforeUpdateHisStentConcludes));
                }
				this.beforeUpdateHisStentConcludes = null;
            }
        }
    }
}
