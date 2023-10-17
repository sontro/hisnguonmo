using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEmotionlessMethod
{
    partial class HisEmotionlessMethodUpdate : BusinessBase
    {
		private HIS_EMOTIONLESS_METHOD beforeUpdateHisEmotionlessMethodDTO;
		private List<HIS_EMOTIONLESS_METHOD> beforeUpdateHisEmotionlessMethodDTOs;
		
        internal HisEmotionlessMethodUpdate()
            : base()
        {

        }

        internal HisEmotionlessMethodUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EMOTIONLESS_METHOD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmotionlessMethodCheck checker = new HisEmotionlessMethodCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EMOTIONLESS_METHOD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.EMOTIONLESS_METHOD_CODE, data.ID);
                if (valid)
                {
					this.beforeUpdateHisEmotionlessMethodDTO = raw;
					if (!DAOWorker.HisEmotionlessMethodDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmotionlessMethod_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEmotionlessMethod that bai." + LogUtil.TraceData("data", data));
                    }
                    
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

        internal bool UpdateList(List<HIS_EMOTIONLESS_METHOD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEmotionlessMethodCheck checker = new HisEmotionlessMethodCheck(param);
                List<HIS_EMOTIONLESS_METHOD> listRaw = new List<HIS_EMOTIONLESS_METHOD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.EMOTIONLESS_METHOD_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisEmotionlessMethodDTOs = listRaw;
					if (!DAOWorker.HisEmotionlessMethodDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmotionlessMethod_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEmotionlessMethod that bai." + LogUtil.TraceData("listData", listData));
                    }
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
            if (this.beforeUpdateHisEmotionlessMethodDTO != null)
            {
                if (!new HisEmotionlessMethodUpdate(param).Update(this.beforeUpdateHisEmotionlessMethodDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisEmotionlessMethod that bai, can kiem tra lai." + LogUtil.TraceData("HisEmotionlessMethodDTO", this.beforeUpdateHisEmotionlessMethodDTO));
                }
            }
			
			if (this.beforeUpdateHisEmotionlessMethodDTOs != null)
            {
                if (!new HisEmotionlessMethodUpdate(param).UpdateList(this.beforeUpdateHisEmotionlessMethodDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisEmotionlessMethod that bai, can kiem tra lai." + LogUtil.TraceData("HisEmotionlessMethodDTOs", this.beforeUpdateHisEmotionlessMethodDTOs));
                }
            }
        }
    }
}
