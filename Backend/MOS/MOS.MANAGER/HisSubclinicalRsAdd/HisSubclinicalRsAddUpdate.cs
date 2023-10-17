using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSubclinicalRsAdd
{
    partial class HisSubclinicalRsAddUpdate : BusinessBase
    {
		private List<HIS_SUBCLINICAL_RS_ADD> beforeUpdateHisSubclinicalRsAdds = new List<HIS_SUBCLINICAL_RS_ADD>();
		
        internal HisSubclinicalRsAddUpdate()
            : base()
        {

        }

        internal HisSubclinicalRsAddUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SUBCLINICAL_RS_ADD data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSubclinicalRsAddCheck checker = new HisSubclinicalRsAddCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SUBCLINICAL_RS_ADD raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisSubclinicalRsAddDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSubclinicalRsAdd_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSubclinicalRsAdd that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisSubclinicalRsAdds.Add(raw);
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

        internal bool UpdateList(List<HIS_SUBCLINICAL_RS_ADD> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSubclinicalRsAddCheck checker = new HisSubclinicalRsAddCheck(param);
                List<HIS_SUBCLINICAL_RS_ADD> listRaw = new List<HIS_SUBCLINICAL_RS_ADD>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisSubclinicalRsAddDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSubclinicalRsAdd_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSubclinicalRsAdd that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisSubclinicalRsAdds.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisSubclinicalRsAdds))
            {
                if (!DAOWorker.HisSubclinicalRsAddDAO.UpdateList(this.beforeUpdateHisSubclinicalRsAdds))
                {
                    LogSystem.Warn("Rollback du lieu HisSubclinicalRsAdd that bai, can kiem tra lai." + LogUtil.TraceData("HisSubclinicalRsAdds", this.beforeUpdateHisSubclinicalRsAdds));
                }
				this.beforeUpdateHisSubclinicalRsAdds = null;
            }
        }
    }
}
