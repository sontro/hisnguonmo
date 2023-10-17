using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEmotionlessResult
{
    partial class HisEmotionlessResultUpdate : BusinessBase
    {
		private List<HIS_EMOTIONLESS_RESULT> beforeUpdateHisEmotionlessResults = new List<HIS_EMOTIONLESS_RESULT>();
		
        internal HisEmotionlessResultUpdate()
            : base()
        {

        }

        internal HisEmotionlessResultUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EMOTIONLESS_RESULT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmotionlessResultCheck checker = new HisEmotionlessResultCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EMOTIONLESS_RESULT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.EMOTIONLESS_RESULT_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisEmotionlessResultDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmotionlessResult_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEmotionlessResult that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisEmotionlessResults.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_EMOTIONLESS_RESULT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEmotionlessResultCheck checker = new HisEmotionlessResultCheck(param);
                List<HIS_EMOTIONLESS_RESULT> listRaw = new List<HIS_EMOTIONLESS_RESULT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.EMOTIONLESS_RESULT_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisEmotionlessResultDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmotionlessResult_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEmotionlessResult that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisEmotionlessResults.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisEmotionlessResults))
            {
                if (!DAOWorker.HisEmotionlessResultDAO.UpdateList(this.beforeUpdateHisEmotionlessResults))
                {
                    LogSystem.Warn("Rollback du lieu HisEmotionlessResult that bai, can kiem tra lai." + LogUtil.TraceData("HisEmotionlessResults", this.beforeUpdateHisEmotionlessResults));
                }
				this.beforeUpdateHisEmotionlessResults = null;
            }
        }
    }
}
