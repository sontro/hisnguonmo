using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicineGroup
{
    partial class HisMedicineGroupUpdate : BusinessBase
    {
		private List<HIS_MEDICINE_GROUP> beforeUpdateHisMedicineGroups = new List<HIS_MEDICINE_GROUP>();
		
        internal HisMedicineGroupUpdate()
            : base()
        {

        }

        internal HisMedicineGroupUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDICINE_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicineGroupCheck checker = new HisMedicineGroupCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEDICINE_GROUP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.MEDICINE_GROUP_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisMedicineGroupDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineGroup_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicineGroup that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisMedicineGroups.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_MEDICINE_GROUP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicineGroupCheck checker = new HisMedicineGroupCheck(param);
                List<HIS_MEDICINE_GROUP> listRaw = new List<HIS_MEDICINE_GROUP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MEDICINE_GROUP_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisMedicineGroupDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicineGroup_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicineGroup that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisMedicineGroups.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMedicineGroups))
            {
                if (!DAOWorker.HisMedicineGroupDAO.UpdateList(this.beforeUpdateHisMedicineGroups))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicineGroup that bai, can kiem tra lai." + LogUtil.TraceData("HisMedicineGroups", this.beforeUpdateHisMedicineGroups));
                }
				this.beforeUpdateHisMedicineGroups = null;
            }
        }
    }
}
