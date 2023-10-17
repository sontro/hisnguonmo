using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRationGroup
{
    partial class HisRationGroupUpdate : BusinessBase
    {
		private List<HIS_RATION_GROUP> beforeUpdateHisRationGroups = new List<HIS_RATION_GROUP>();
		
        internal HisRationGroupUpdate()
            : base()
        {

        }

        internal HisRationGroupUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_RATION_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRationGroupCheck checker = new HisRationGroupCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_RATION_GROUP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisRationGroupDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationGroup_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRationGroup that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisRationGroups.Add(raw);
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

        internal bool UpdateList(List<HIS_RATION_GROUP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRationGroupCheck checker = new HisRationGroupCheck(param);
                List<HIS_RATION_GROUP> listRaw = new List<HIS_RATION_GROUP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisRationGroupDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRationGroup_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRationGroup that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisRationGroups.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisRationGroups))
            {
                if (!DAOWorker.HisRationGroupDAO.UpdateList(this.beforeUpdateHisRationGroups))
                {
                    LogSystem.Warn("Rollback du lieu HisRationGroup that bai, can kiem tra lai." + LogUtil.TraceData("HisRationGroups", this.beforeUpdateHisRationGroups));
                }
				this.beforeUpdateHisRationGroups = null;
            }
        }
    }
}
