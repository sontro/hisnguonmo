using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBloodGroup
{
    partial class HisBloodGroupUpdate : BusinessBase
    {
		private List<HIS_BLOOD_GROUP> beforeUpdateHisBloodGroups = new List<HIS_BLOOD_GROUP>();
		
        internal HisBloodGroupUpdate()
            : base()
        {

        }

        internal HisBloodGroupUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_BLOOD_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBloodGroupCheck checker = new HisBloodGroupCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_BLOOD_GROUP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.BLOOD_GROUP_CODE, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisBloodGroups.Add(raw);
					if (!DAOWorker.HisBloodGroupDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBloodGroup_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBloodGroup that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_BLOOD_GROUP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisBloodGroupCheck checker = new HisBloodGroupCheck(param);
                List<HIS_BLOOD_GROUP> listRaw = new List<HIS_BLOOD_GROUP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.BLOOD_GROUP_CODE, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisBloodGroups.AddRange(listRaw);
					if (!DAOWorker.HisBloodGroupDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBloodGroup_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBloodGroup that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisBloodGroups))
            {
                if (!new HisBloodGroupUpdate(param).UpdateList(this.beforeUpdateHisBloodGroups))
                {
                    LogSystem.Warn("Rollback du lieu HisBloodGroup that bai, can kiem tra lai." + LogUtil.TraceData("HisBloodGroups", this.beforeUpdateHisBloodGroups));
                }
            }
        }
    }
}
