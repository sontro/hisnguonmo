using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisDebateTemp
{
    partial class HisDebateTempUpdate : BusinessBase
    {
		private List<HIS_DEBATE_TEMP> beforeUpdateHisDebateTemps = new List<HIS_DEBATE_TEMP>();
		
        internal HisDebateTempUpdate()
            : base()
        {

        }

        internal HisDebateTempUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_DEBATE_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisDebateTempCheck checker = new HisDebateTempCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_DEBATE_TEMP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisDebateTempDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebateTemp_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDebateTemp that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisDebateTemps.Add(raw);
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

        internal bool UpdateList(List<HIS_DEBATE_TEMP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisDebateTempCheck checker = new HisDebateTempCheck(param);
                List<HIS_DEBATE_TEMP> listRaw = new List<HIS_DEBATE_TEMP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisDebateTempDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisDebateTemp_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisDebateTemp that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisDebateTemps.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisDebateTemps))
            {
                if (!DAOWorker.HisDebateTempDAO.UpdateList(this.beforeUpdateHisDebateTemps))
                {
                    LogSystem.Warn("Rollback du lieu HisDebateTemp that bai, can kiem tra lai." + LogUtil.TraceData("HisDebateTemps", this.beforeUpdateHisDebateTemps));
                }
				this.beforeUpdateHisDebateTemps = null;
            }
        }
    }
}
