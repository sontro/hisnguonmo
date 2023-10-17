using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEquipmentSetMaty
{
    partial class HisEquipmentSetMatyUpdate : BusinessBase
    {
		private List<HIS_EQUIPMENT_SET_MATY> beforeUpdateHisEquipmentSetMatys = new List<HIS_EQUIPMENT_SET_MATY>();
		
        internal HisEquipmentSetMatyUpdate()
            : base()
        {

        }

        internal HisEquipmentSetMatyUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EQUIPMENT_SET_MATY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEquipmentSetMatyCheck checker = new HisEquipmentSetMatyCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EQUIPMENT_SET_MATY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisEquipmentSetMatyDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEquipmentSetMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEquipmentSetMaty that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisEquipmentSetMatys.Add(raw);
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

        internal bool UpdateList(List<HIS_EQUIPMENT_SET_MATY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEquipmentSetMatyCheck checker = new HisEquipmentSetMatyCheck(param);
                List<HIS_EQUIPMENT_SET_MATY> listRaw = new List<HIS_EQUIPMENT_SET_MATY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisEquipmentSetMatyDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEquipmentSetMaty_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEquipmentSetMaty that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisEquipmentSetMatys.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisEquipmentSetMatys))
            {
                if (!DAOWorker.HisEquipmentSetMatyDAO.UpdateList(this.beforeUpdateHisEquipmentSetMatys))
                {
                    LogSystem.Warn("Rollback du lieu HisEquipmentSetMaty that bai, can kiem tra lai." + LogUtil.TraceData("HisEquipmentSetMatys", this.beforeUpdateHisEquipmentSetMatys));
                }
				this.beforeUpdateHisEquipmentSetMatys = null;
            }
        }
    }
}
