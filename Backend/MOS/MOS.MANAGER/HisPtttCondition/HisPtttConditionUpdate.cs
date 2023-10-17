using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisPtttCondition
{
    partial class HisPtttConditionUpdate : BusinessBase
    {
		private HIS_PTTT_CONDITION beforeUpdateHisPtttConditionDTO;
		private List<HIS_PTTT_CONDITION> beforeUpdateHisPtttConditionDTOs;
		
        internal HisPtttConditionUpdate()
            : base()
        {

        }

        internal HisPtttConditionUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_PTTT_CONDITION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisPtttConditionCheck checker = new HisPtttConditionCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_PTTT_CONDITION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.PTTT_CONDITION_CODE, data.ID);
                if (valid)
                {
					this.beforeUpdateHisPtttConditionDTO = raw;
					if (!DAOWorker.HisPtttConditionDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttCondition_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPtttCondition that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_PTTT_CONDITION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisPtttConditionCheck checker = new HisPtttConditionCheck(param);
                List<HIS_PTTT_CONDITION> listRaw = new List<HIS_PTTT_CONDITION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.PTTT_CONDITION_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisPtttConditionDTOs = listRaw;
					if (!DAOWorker.HisPtttConditionDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPtttCondition_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisPtttCondition that bai." + LogUtil.TraceData("listData", listData));
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
            if (this.beforeUpdateHisPtttConditionDTO != null)
            {
                if (!new HisPtttConditionUpdate(param).Update(this.beforeUpdateHisPtttConditionDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisPtttCondition that bai, can kiem tra lai." + LogUtil.TraceData("HisPtttConditionDTO", this.beforeUpdateHisPtttConditionDTO));
                }
            }
			
			if (this.beforeUpdateHisPtttConditionDTOs != null)
            {
                if (!new HisPtttConditionUpdate(param).UpdateList(this.beforeUpdateHisPtttConditionDTOs))
                {
                    LogSystem.Warn("Rollback du lieu HisPtttCondition that bai, can kiem tra lai." + LogUtil.TraceData("HisPtttConditionDTOs", this.beforeUpdateHisPtttConditionDTOs));
                }
            }
        }
    }
}
