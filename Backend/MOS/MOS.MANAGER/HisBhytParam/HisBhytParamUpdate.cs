using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBhytParam
{
    partial class HisBhytParamUpdate : BusinessBase
    {
		private List<HIS_BHYT_PARAM> beforeUpdateHisBhytParams = new List<HIS_BHYT_PARAM>();
		
        internal HisBhytParamUpdate()
            : base()
        {

        }

        internal HisBhytParamUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BHYT_PARAM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBhytParamCheck checker = new HisBhytParamCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BHYT_PARAM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisBhytParamDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBhytParam_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBhytParam that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisBhytParams.Add(raw);
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

        internal bool UpdateList(List<HIS_BHYT_PARAM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBhytParamCheck checker = new HisBhytParamCheck(param);
                List<HIS_BHYT_PARAM> listRaw = new List<HIS_BHYT_PARAM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisBhytParamDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBhytParam_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBhytParam that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisBhytParams.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBhytParams))
            {
                if (!DAOWorker.HisBhytParamDAO.UpdateList(this.beforeUpdateHisBhytParams))
                {
                    LogSystem.Warn("Rollback du lieu HisBhytParam that bai, can kiem tra lai." + LogUtil.TraceData("HisBhytParams", this.beforeUpdateHisBhytParams));
                }
				this.beforeUpdateHisBhytParams = null;
            }
        }
    }
}
